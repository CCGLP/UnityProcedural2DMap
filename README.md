# UnityProcedural2DMap
An easy to use Procedural 2D map created in Unity. 
Works in all versions of Unity, Demo Project updated to Unity 2021.2.0f1

![](https://i.gyazo.com/4b324d03ba9f79400d63873275fde612.png)

## Use 
Create a new GameObject and add the component “ProceduralMap”. 

Change the map size, edit at will the properties and play the scene or click “Generate”.
The property “CollisionPrefab” will take a prefab, in most cases you would like to add
a Prefab with a SpriteRenderer and a BoxCollider2D. 

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

