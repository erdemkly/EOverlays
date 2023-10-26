# EOverlays

EOverlays is a editor tool for Unity 3D game engine to write custom editor tools & utilities. 
EOverlay uses Overlays at Unity (you can discover overlays from [this link](https://docs.unity3d.com/Manual/overlays.html)).

## Installation

### Git

For git installation you need to know git link (https://github.com/erdemkly/EOverlays.git)
Paste this link to<br>
<b>Window -> Package Manager -> + -> Add package from git url -> [Paste link here] -> Add</b><br>
(The location of this feature may vary depending on the Unity version. I am using version 2021.3.21f1 of the Unity game engine.)

### Install via OpenUPM

The package is available on the [openupm registry](https://openupm.com). It's recommended to install it via [openupm-cli](https://github.com/openupm/openupm-cli).

```
openupm add com.erdem.overlays
```

## Usage

#### Your scripts must be children of the Editor folder & Editor folder must have Assembly Definition Reference of EOverlays.
You can discover some usages from Samples in the package.

### Attribute
You must add this attribute to method to serialize.
```csharp
 [EOverlayElement(name:"Tab Name",order:0,enableCondition:"condition_variable_name")]
```
#### Enable Condition
If you want to control your methods visibility with bool variable you can use enable condition parameter.
```csharp
public static bool EnableCondition {get; set;}
 [EOverlayElement(name:"Tab Name",order:0,enableCondition:"EnableCondition")]
```

### Methods

Your methods must be static and public to work correctly.
<details>
  <summary>Supported Types</summary>
  
  ```csharp
 void, int, float, double,
long, Enum, Object, Vector2,
Vector3, Vector2Int, Vector3Int, string, Color
```
For now.

</details>

```csharp
 [EOverlayElement(name:"Tab Name",order:0,enableCondition:"condition_variable_name")]
public static Type MyMethod(Type a,Type b,Type c){}
```



