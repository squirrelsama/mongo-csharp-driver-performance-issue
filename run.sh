#!/bin/bash
rm times 
rm details
xbuild \
&& MongoDriverPerf.1.6.1/bin/Debug/MongoDriverPerf.1.6.1.exe $1 $2 > >(tee -a times) 2> >(tee -a details >&2) \
&& MongoDriverPerf.1.7.0/bin/Debug/MongoDriverPerf.1.7.0.exe $1 $2 > >(tee -a times) 2> >(tee -a details >&2) \
&& MongoDriverPerf.1.7.1/bin/Debug/MongoDriverPerf.1.7.1.exe $1 $2 > >(tee -a times) 2> >(tee -a details >&2) \
&& MongoDriverPerf.1.8.0/bin/Debug/MongoDriverPerf.1.8.0.exe $1 $2 > >(tee -a times) 2> >(tee -a details >&2) \
&& MongoDriverPerf.1.8.1/bin/Debug/MongoDriverPerf.1.8.1.exe $1 $2 > >(tee -a times) 2> >(tee -a details >&2) \
&& MongoDriverPerf.1.8.2/bin/Debug/MongoDriverPerf.1.8.2.exe $1 $2 > >(tee -a times) 2> >(tee -a details >&2) \
&& MongoDriverPerf.1.8.3/bin/Debug/MongoDriverPerf.1.8.3.exe $1 $2 > >(tee -a times) 2> >(tee -a details >&2) \
&& MongoDriverPerf.1.9.0/bin/Debug/MongoDriverPerf.1.9.0.exe $1 $2 > >(tee -a times) 2> >(tee -a details >&2) \
&& MongoDriverPerf.1.9.1/bin/Debug/MongoDriverPerf.1.9.1.exe $1 $2 > >(tee -a times) 2> >(tee -a details >&2) \
&& MongoDriverPerf.1.9.2/bin/Debug/MongoDriverPerf.1.9.2.exe $1 $2 > >(tee -a times) 2> >(tee -a details >&2)
