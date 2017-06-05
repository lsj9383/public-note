#ifndef __TEST_MODEL_H
#define __TEST_MODEL_H

#include <iostream>
#include <cv.h>
#include <opencv.hpp>
#include <opencv2\core\core.hpp>
#include <opencv2/highgui/highgui.hpp>
#include <opencv2\ml\ml.hpp>

#define thickness	-1
#define lineType	8

const int trainSize = 500;

const cv::Vec3b red(0, 0, 255);
const cv::Vec3b green(0, 255, 0);
const cv::Vec3b blue(255, 0, 0);

//(x, y)
const float classCenter1[2] = {300, 100};
const float classCenter2[2] = {100, 300};

void test_bayes(cv::Mat &space, float (&trains)[trainSize][2], float (&labels)[trainSize]);
void test_knn(cv::Mat &space, float (&trains)[trainSize][2], float (&labels)[trainSize]);
void test_svm(cv::Mat &space, float (&trains)[trainSize][2], float (&labels)[trainSize]);
void test_dtree(cv::Mat &space, float (&trains)[trainSize][2], float (&labels)[trainSize]);
void test_boosting(cv::Mat &space, float (&trains)[trainSize][2], float (&labels)[trainSize]);
void test_rt(cv::Mat &space, float (&trains)[trainSize][2], float (&labels)[trainSize]);

#endif