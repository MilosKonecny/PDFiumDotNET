#pragma once
#include<Windows.h>

class ProgressBar
{
private:
	HANDLE _hStandardOutput;
	int _progressBarSize;
	int _maxValue;
	int _actualValue;
	int _positionX;
	int _positionY;

public:
	ProgressBar();
	void Start(int progressBarSize, int maxValue);
	void Stop();
	void Step();

private:
	void SetCursorPosition();
	void ShowProgress();
	void ClearProgress();
};

