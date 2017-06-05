#include "test_ model.h"

using namespace cv;
using namespace std;

void test_boosting(Mat &space, float (&trains)[trainSize][2], float (&labels)[trainSize])
{

	imshow("space", space);
	waitKey(0);
}