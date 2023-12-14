#include "ProgressBar.h"
#include <WinBase.h>
#include <consoleapi2.h>
#include <string>
#include <iostream>
#include <set>

ProgressBar::ProgressBar()
{
	_hStandardOutput = INVALID_HANDLE_VALUE;
	_progressBarSize = 0;
	_maxValue = 0;
	_actualValue = 0;
	_positionX = 0;
	_positionY = 0;
}

void ProgressBar::Start(int progressBarSize, int maxValue)
{
	_progressBarSize = progressBarSize;
	_maxValue = maxValue;
	_actualValue = 0;
	_hStandardOutput = ::GetStdHandle(STD_OUTPUT_HANDLE);
	if (_hStandardOutput == INVALID_HANDLE_VALUE)
	{
		return;
	}

	CONSOLE_SCREEN_BUFFER_INFO consoleInfo;
	if (!GetConsoleScreenBufferInfo(_hStandardOutput, &consoleInfo))
	{
		_hStandardOutput = INVALID_HANDLE_VALUE;
		return;
	}

	_positionX = consoleInfo.dwCursorPosition.X;
	_positionY = consoleInfo.dwCursorPosition.Y;

	ShowProgress();
}

void ProgressBar::Step()
{
	if (_hStandardOutput == INVALID_HANDLE_VALUE)
	{
		return;
	}

	_actualValue = min(_actualValue + 1, _maxValue);
	SetCursorPosition();
	ShowProgress();
}

void ProgressBar::Stop()
{
	SetCursorPosition();
	ClearProgress();
	SetCursorPosition();
	_hStandardOutput = INVALID_HANDLE_VALUE;
}

void ProgressBar::SetCursorPosition()
{
	COORD cursorPosition;
	cursorPosition.X = _positionX;
	cursorPosition.Y = _positionY;
	SetConsoleCursorPosition(_hStandardOutput, cursorPosition);
}

void ProgressBar::ShowProgress()
{
	std::string text;
	text.append("[");
	for (auto index = 0; index < _progressBarSize; index++)
	{
		if (_progressBarSize * _actualValue / _maxValue > index)
		{
			text.append("*");
		}
		else
		{
			text.append(" ");
		}
	}
	text.append("]");
	std::cout << text;
}

void ProgressBar::ClearProgress()
{
	std::string text(_progressBarSize + 2, ' ');
	std::cout << text;
}