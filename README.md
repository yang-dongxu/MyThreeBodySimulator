# MyThreeBodySimulator

## introduction
this is a app for simulate three body by calculate positions numeric, 
follows F=GmM/r\*r, and the collison between stars are treaded as no energy cost

its consists of three part.
First, the MyThreeBodySimulator.dll is the core, which responsible for get positions of starts, and echa line represents a start.In other words, you can simulate more than **THREE** if your cpu is ok.
Second, the MyWebServerForThreeBody.exe is a web server which call the dll before to process the query and send back positions information
Third, the InitializeConfig.csv is a config file to record initial state of positions.

## usage
* edit the InitializeConfig.csv, by the formate of below:

    x,y,z,vx,vy,vz,mass,size
    
    (x,y,z are the postions of start, and vx,vy,vz are its speed, all these should be float or integer)

* start MyWebServerForThreeBody.exe
* find a client, which can tcp link to the server at port 8899, and send your query in json.  Then, positions will give back in json. By the way, i give a simple client here, which is simple to use, but it's really uguly. So, build your owns.

