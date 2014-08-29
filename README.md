MongoDB C# Driver Performance Demonstartion
=========

This is a demonstration of some gradual performance issues I've observed with the MongoDB C# Driver. Between versions 1.6.1 and 1.9.2, the performance has approximately halved. 

My team noticed this when they deployed an upgrade from 1.6.1 to 1.9.2, and had to roll back because of serious performance issues including massively increased response times and maxed out open connections. The issue was isolated to this Driver upgrade.

The related Jira ticket is https://jira.mongodb.org/browse/CSHARP-1053

First, let's
--

  - Make sure mongo is installed, running, and accepts connections without authentication.
  - Make sure wherever your mongo directory is, you have several GB free for this demonstration.

Implementation Notes
--

  - The code for the 1.6.1 demonstration is how we'd previously used the driver.
  - All other code for 1.7.0-1.9.2 is different from the 1.6.1 version, but is the same between them. It reflects a breaking API update introducted in 1.7.0.

Run It!
--

Each of the programs will take two arguments.
  - Threads, defaulted to 100.
  - Operations, which causes N creations and N reads, defaulted to 10,000.
  - I used the defaults for all my results, and I recommend you do not use lower numbers than those when you run this.
  
If you are on *nix, I have a shell script run.sh for you.

    ./run.sh 100 10000 

I'm sure an equivalent powershell script would be trivial to write.

Notes
--

I am running

  - Arch Linux x64
  - Mono 3.2.8
  - Mongod 2.6.4
  - Intel Core i7-4790K CPU @ 4.00GHz

My Results
--
Gleaned from the output file named "times".

  - 1.6.1.4678 took 28314ms
  - 1.7.0.4714 took 33085ms
  - 1.7.1.4791 took 33932ms
  - 1.8.0.124 took 36391ms
  - 1.8.1.20 took 38995ms
  - 1.8.2.34 took 41144ms
  - 1.8.3.9 took 40477ms
  - 1.9.0.200 took 59239ms
  - 1.9.1.221 took 60077ms
  - 1.9.2.235 took 59171ms

Final Notes
--
If you look at the output file named "details", you'll see the rate at which threads are opened and terminate, in addition to output from mongostat. 

You'll notice the netIn/netOut numbers are also inversely correlated with the response times (and version numbers), suggesting the driver cannot push data to the MongoDB server fast enough. e.g.
> v1.6.1: 7m netIn / 5m netOut
>
> v1.9.2: 3m netIn / 1m netOut

Also before you say so: No, my numbers do not result from increasing DB size. You can run those in reverse and see the same output. :P


