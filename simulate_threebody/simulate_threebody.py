
import clr;
import sys;
import os;

#clr.AddReference('MyThreeBodySimulator');

clr.AddReference("trypythonnet");
clr.AddReference('myPlanet');
clr.AddReference('System.Numerics.Vectors')

import System


for i in clr.ListAssemblies(False):
    print(i)

from System.Numerics.Vectors import *
from  tryofpythonnet import *
#from  MySimulatorOfThreeBody import *

Class1.hello("ppp")
#Class2.goodbye()

#import MySimulatorOfThreeBody;
print("success")