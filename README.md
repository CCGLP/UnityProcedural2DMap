# UnityProcedural2DMap
An easy to use Procedural 2D map created in Unity. 


## Use 
Create a new GameObject and add the component “ProceduralMap”. 

Change the map size, edit at will the properties and play the scene or click “Generate”.


To generate a new map in another script add the next using directive : 



``` c#
  using ccglp.Procedural;
```

Take the reference to ProceduralMap and call InitializeAndGenerateMap(): 


``` c#
  ProceduralMap map = GameObject.Find(“MapGenerator”).GetComponent<ProceduralMap>();
  map.InitiliazeAndGenerateMap();
```

Enjoy!

![](https://i.gyazo.com/4dba5bbd8bf94ac5b5004eca92fd3205.gif)

