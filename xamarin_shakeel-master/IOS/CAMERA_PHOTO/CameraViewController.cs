using Foundation;
using System;
using UIKit;
using AVFoundation;
using CoreFoundation;
using CoreMedia;
using CoreGraphics;
using CoreVideo;
using Photos;
using System.IO;

namespace FaceCareApp
{
    public partial class CameraViewController : UIViewController
    {
        public CameraViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            this.imageViewVideo.BackgroundColor = UIColor.Black;    //设置背景色
            //this.imageViewVideo.ContentMode = UIViewContentMode.ScaleAspectFit;

            //View显示处理后就开始显示视频了，这个按钮的功能是保存照片
            this.btnSnap.TouchUpInside += BtnSnap_TouchUpInside;
            this.btnSnap.ImageView.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
        }

        public override void ViewWillAppear(bool animated)
        {   //View显示的时候，就配置好
            base.ViewWillAppear(animated);
            //先处理基类的ViewWillAppear
            this.TabBarController.TabBar.Hidden = true;
            SetupCaptureSession();                                //配置完了就开整
        }

        public override void ViewWillDisappear(bool animated)
        {   //View消失的时候，就停止
            base.ViewWillDisappear(animated);
            this.TabBarController.TabBar.Hidden = false;
            StopCaptureSession();
        }

        private DispatchQueue m_queue = new DispatchQueue("myQueue");
        private AVCaptureSession m_session = new AVCaptureSession();            //其中包含了视频源，以及视频输出
        private AVCaptureDevice m_videoDevice = null;                           //视频源的设备，这里使用的是摄像头
        private AVCaptureStillImageOutput m_stillImageOutput = null;            //这是session其中的一个输出，该输出被jpeg压缩过

        async void SetupCaptureSession()
        {
            var granted = await AVCaptureDevice.RequestAccessForMediaTypeAsync(AVMediaType.Video);
            if (granted)
            {
                //AVCaptureDevice captureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
                AVCaptureDevice videoDevice = null;
                AVCaptureDevice[] devices = AVCaptureDevice.DevicesWithMediaType(AVMediaType.Video);
                foreach (var item in devices)
                {
                    if (item.Position == AVCaptureDevicePosition.Back)
                    {
                        videoDevice = item;
                        break;
                    }
                }
                if (videoDevice == null)
                {
                    new UIAlertView("提示", "获取摄像头失败！", null, "确定").Show();
                    return;
                }

                AVCaptureDeviceInput videoInput = AVCaptureDeviceInput.FromDevice(videoDevice); //视频源即为摄像头
                if (videoInput == null)
                {
                    new UIAlertView("提示", "获取摄像头视频源失败！", null, "确定").Show();
                    return;
                }

                AVCaptureVideoDataOutput videoOutput = new AVCaptureVideoDataOutput();
                videoOutput.WeakVideoSettings = new CVPixelBufferAttributes() { PixelFormatType = CVPixelFormatType.CV32BGRA }.Dictionary;
                videoOutput.MinFrameDuration = new CMTime(1, 15);  // 15fps
                videoOutput.SetSampleBufferDelegateQueue(new CameraVideoTransform(this.imageViewVideo), m_queue);       //输出到imageViewVideo,并且每一帧由CameraVideoTransform类做一个变换

                AVCaptureStillImageOutput stillImageOutput = new AVCaptureStillImageOutput();
                stillImageOutput.CompressedVideoSetting = new AVVideoSettingsCompressed() { Codec = AVVideoCodec.JPEG };

                m_session.BeginConfiguration();
                m_session.SessionPreset = AVCaptureSession.PresetMedium;
                m_session.AddInput(videoInput);
                //配置了两个输出
                //videoOutput，委托给CameraVideoTransform类进行.
                //stillImageOutput, 没有委托给它输出，但是它设置了输出的格式是被jpeg压缩过的.
                m_session.AddOutput(videoOutput);
                m_session.AddOutput(stillImageOutput);
                m_session.CommitConfiguration();

                m_queue.DispatchAsync(delegate ()
                {
                    m_session.StartRunning();       //开整
                });

                m_videoDevice = videoDevice;
                m_stillImageOutput = stillImageOutput;
            }
            else
            {
                new UIAlertView("提示", "没有访问摄像头的权限！", null, "确定").Show();
                //this.NavigationController.PopViewController(true);
                return;
            }
        }

        void StopCaptureSession()
        {
            m_queue.DispatchAsync(delegate()
            {
                m_session.StopRunning();
            });
        }

        private void BtnSnap_TouchUpInside(object sender, EventArgs e)
        {
            SnapStillImage();
        }

        private async void SnapStillImage()
        {
            //
            if ((m_videoDevice != null) && (m_stillImageOutput != null))
            {
                if (m_videoDevice.HasFlash && m_videoDevice.IsFlashModeSupported(AVCaptureFlashMode.Auto))
                {
                    NSError error;
                    if (m_videoDevice.LockForConfiguration(out error))
                    {
                        m_videoDevice.FlashMode = AVCaptureFlashMode.Auto;
                        m_videoDevice.UnlockForConfiguration();
                    }
                }

                AVCaptureConnection connection = m_stillImageOutput.ConnectionFromMediaType(AVMediaType.Video);
                var imageDataSampleBuffer = await m_stillImageOutput.CaptureStillImageTaskAsync(connection);        //获得当前帧的压缩图像
                var imageData = AVCaptureStillImageOutput.JpegStillToNSData(imageDataSampleBuffer);                 //得到当前帧压缩图像的图像数据...

                //RequestAuthorization(handler), handler是用户与权限对话框交互后，执行的动作。
                PHPhotoLibrary.RequestAuthorization(status => {
                    
                    if (status == PHAuthorizationStatus.Authorized)
                    {   // 若用户授权了

                        // To preserve the metadata, we create an asset from the JPEG NSData representation.
                        // Note that creating an asset from a UIImage discards the metadata.

                        // In iOS 9, we can use AddResource method on PHAssetCreationRequest class.
                        // In iOS 8, we save the image to a temporary file and use +[PHAssetChangeRequest creationRequestForAssetFromImageAtFileURL:].

                        if (UIDevice.CurrentDevice.CheckSystemVersion(9, 0))
                        {
                            //PHPhotoLibrary.SharedPhotoLibrary 返回的是一个(共享)图片库对象
                            //PerformChanges (changeHandler, completionHandler) changeHandler 以及 completionHandler 是一个lambda
                            PHPhotoLibrary.SharedPhotoLibrary.PerformChanges(() => {
                                var request = PHAssetCreationRequest.CreationRequestForAsset();
                                request.AddResource(PHAssetResourceType.Photo, imageData, null);        //保存当前照片
                            }, (success, err) => {
                                if (!success)
                                {
                                    Console.WriteLine("Error occurred while saving image to photo library: {0}", err);
                                }
                            });
                        }
                        else
                        {   //用户没有授权

                            string outputFileName = NSProcessInfo.ProcessInfo.GloballyUniqueString;
                            string tmpDir = Path.GetTempPath();
                            string outputFilePath = Path.Combine(tmpDir, outputFileName);
                            string outputFilePath2 = Path.ChangeExtension(outputFilePath, "jpg");
                            NSUrl temporaryFileUrl = new NSUrl(outputFilePath2, false);

                            PHPhotoLibrary.SharedPhotoLibrary.PerformChanges(() => {
                                NSError error = null;
                                if (imageData.Save(temporaryFileUrl, NSDataWritingOptions.Atomic, out error))
                                {
                                    PHAssetChangeRequest.FromImage(temporaryFileUrl);
                                }
                                else
                                {
                                    Console.WriteLine("Error occured while writing image data to a temporary file: {0}", error);
                                }
                            }, (success, error) => {
                                if (!success)
                                {
                                    Console.WriteLine("Error occurred while saving image to photo library: {0}", error);
                                }

                                // Delete the temporary file.
                                NSError deleteError;
                                NSFileManager.DefaultManager.Remove(temporaryFileUrl, out deleteError);
                            });
                        }
                    }
                });
            }
        }
    }

    public class CameraVideoTransform : AVCaptureVideoDataOutputSampleBufferDelegate
    {
        private UIImageView m_imageView = null;

        public CameraVideoTransform(UIImageView imageView)
        {
            m_imageView = imageView;
        }

        //
        public override void DidOutputSampleBuffer(AVCaptureOutput captureOutput, CMSampleBuffer sampleBuffer, AVCaptureConnection connection)
        {   //可以从缓冲区中输出一帧图像时，将会调用该方法
            try
            {
                //从缓冲区中提取一帧图像
                var image = ImageFromSampleBuffer(sampleBuffer);

                /*
                 * 
                 * do some processing
                 *
                 */
                
                //显示当前帧
                m_imageView.BeginInvokeOnMainThread(delegate()
                {
                    m_imageView.Image = image;
                });

                sampleBuffer.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        UIImage ImageFromSampleBuffer(CMSampleBuffer sampleBuffer)
        {
            UIImage frame = null;

            // Get a CMSampleBuffer's Core Video image buffer for the media data
            using (var pixelBuffer = sampleBuffer.GetImageBuffer() as CVPixelBuffer)
            {
                // Lock the base address of the pixel buffer
                pixelBuffer.Lock(CVPixelBufferLock.None);

                IntPtr baseAddress = pixelBuffer.BaseAddress;
                int nWidth = (int)pixelBuffer.Width;
                int nHeight = (int)pixelBuffer.Height;
                int nBytesPerRow = (int)pixelBuffer.BytesPerRow;

                using (var colorSpace = CGColorSpace.CreateDeviceRGB())
                using (var context = new CGBitmapContext(baseAddress, nWidth, nHeight, 8, nBytesPerRow, colorSpace, CGBitmapFlags.PremultipliedFirst | CGBitmapFlags.ByteOrder32Little))
                using (var cgImage = context.ToImage())
                {
                    frame = UIImage.FromImage(cgImage);

                    // Unlock the pixel buffer
                    pixelBuffer.Unlock(CVPixelBufferLock.None);
                }
            }

            return frame;
        }
    }
}