#include "test_ model.h"

using namespace cv;
using namespace std;

void test_knn(Mat &space, float (&trains)[trainSize][2], float (&labels)[trainSize])
{
	Mat labelsMat(trainSize, 1, CV_32FC1, labels);			//使用labels进行labelsMat的初始化
	Mat trainsMat(trainSize, 2, CV_32FC1, trains);

	CvKNearest model;

	model.train(trainsMat, labelsMat, Mat());

	//测试输入

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