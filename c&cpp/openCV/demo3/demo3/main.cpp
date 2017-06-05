#include <opencv2/core/core.hpp>  
#include <opencv2/highgui/highgui.hpp>  
#include <opencv2/ml/ml.hpp>  
#include <iostream>  
#include <string> 

const int FEAT_NUM = 2;
const int SAMPLE_NUM = 4;
  
using namespace std;  
using namespace cv;  
  
int main()  
{  
//    //Setup the BPNetwork  
//    CvANN_MLP bp;   
//    // Set up BPNetwork's parameters  
//    CvANN_MLP_TrainParams params;  
//    params.train_method=CvANN_MLP_TrainParams::BACKPROP;  
//    params.bp_dw_scale=0.1;  
//    params.bp_moment_scale=0.1;  
//    //params.train_method=CvANN_MLP_TrainParams::RPROP;  
//    //params.rp_dw0 = 0.1;   
//    //params.rp_dw_plus = 1.2;   
//    //params.rp_dw_minus = 0.5;  
//    //params.rp_dw_min = FLT_EPSILON;   
//    //params.rp_dw_max = 50.;  
//  
//    // Set up training data  
//    float labels[SAMPLE_NUM] = {1.0, -1.0, -1.0, 1.0};
//	float trainingData[SAMPLE_NUM][FEAT_NUM] = { {501,  10}, {255,  10}, {501, 255}, {10, 501} };  
//
//    Mat labelsMat(SAMPLE_NUM, 1, CV_32FC1, labels);  
//    Mat trainingDataMat(SAMPLE_NUM, FEAT_NUM, CV_32FC1, trainingData);  
//
//    Mat layerSizes=(Mat_<int>(1,3) << FEAT_NUM,3,1);  
//    bp.create(layerSizes,CvANN_MLP::SIGMOID_SYM);//CvANN_MLP::SIGMOID_SYM  
//                                               //CvANN_MLP::GAUSSIAN  
//                                               //CvANN_MLP::IDENTITY  
//    bp.train(trainingDataMat, labelsMat, Mat(),Mat(), params);  
//  
//  
//    // Data for visual representation  
//    int width = 512, height = 512;  
//    Mat image = Mat::zeros(height, width, CV_8UC3);  
//    Vec3b green(0,255,0), blue (255,0,0);  
//    // Show the decision regions given by the SVM  
//    for (int i = 0; i < image.rows; ++i)  
//        for (int j = 0; j < image.cols; ++j)  
//        {  
//            Mat sampleMat = (Mat_<float>(1,2) << i,j);  
//            Mat responseMat;
//			bp.predict(sampleMat, responseMat);
//			float response = *responseMat.ptr<float>(0);
//            
//            if (response >0)  
//                image.at<Vec3b>(j, i)  = green;  
//            else    
//                image.at<Vec3b>(j, i)  = blue;  
//        }  
//  
//        // Show the training data  
//        int thickness = -1;  
//        int lineType = 8;  
//        circle( image, Point(501,  10), 5, Scalar(  0,   0,   0), thickness, lineType);  
//        circle( image, Point(255,  10), 5, Scalar(255, 255, 255), thickness, lineType);  
//        circle( image, Point(501, 255), 5, Scalar(255, 255, 255), thickness, lineType);  
//        circle( image, Point( 10, 501), 5, Scalar(  0,   0,   0), thickness, lineType);  
//  
//        imwrite("result.png", image);        // save the image   
//  
//        imshow("BP Simple Example", image); // show it to the user  
//        waitKey(0);  
//
	CvANN_MLP *bp = new CvANN_MLP;
	delete bp;
}