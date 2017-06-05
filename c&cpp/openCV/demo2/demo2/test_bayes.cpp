#include "test_ model.h"

using namespace cv;
using namespace std;

void test_bayes(Mat &space, float (&trains)[trainSize][2], float (&labels)[trainSize])
{
	Mat labelsMat(trainSize, 1, CV_32FC1, labels);			//使用labels进行labelsMat的初始化
	Mat trainsMat(trainSize, 2, CV_32FC1, trains);

	CvNormalBayesClassifier model;

	model.train(trainsMat, labelsMat, Mat(), Mat());

	//测试输入
	for(int i=0; i<space.rows; i++)
	{
		for(int j=0; j<space.cols; j++)
		{
			Mat sampleMat = (Mat_<float>(1,2)<<i, j);
			float response = model.predict(sampleMat);

			if(response==1)
				space.at<Vec3b>(j, i) = Vec3b(128, 128, 128);
			else
				space.at<Vec3b>(j, i) = blue;
		}
	}

	for(int i=0; i<trainSize; i++)
	{
		if(labels[i] == 1)
			circle( space, Point(trains[i][0],  trains[i][1]), 3, Scalar(0,   0,   255), thickness, lineType);
		else
			circle( space, Point(trains[i][0],  trains[i][1]), 3, Scalar(0,   255,   0), thickness, lineType);
	}

	imshow("space", space);
	waitKey(0);
}