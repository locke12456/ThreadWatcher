#ifndef _READWORKER_H

#define _READWORKER_H
#include <windows.h>
#include <direct.h>
#ifndef LINUX
#define GetCurrentDir _getcwd
#endif
#include <stdlib.h>
#include <stdio.h>
#include <iostream>
#include <fstream>
#include <string>
#include <io.h>
#include <sys/stat.h>
#include "dirent.h"
#include "DataQueue.h"
void _reader_worker(void *);

#endif