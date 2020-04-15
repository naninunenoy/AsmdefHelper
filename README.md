# Asmdef Helper

<img src="https://user-images.githubusercontent.com/15327448/79349410-24950780-7f71-11ea-94be-056726828ec4.png" width="140" />

Unity assembly definition utilities.

This library solve inconvenience of assembly definition on unity.

## Dependency Graph

Unity assembly definition viewer.

[Window] > [Asmdef Helper] > [Open DependencyGraph]

<img src="https://user-images.githubusercontent.com/15327448/79340438-45eff680-7f65-11ea-88f2-dd90157b5df3.png" width="400" />

Show assembly definition referances in your project like this.

<img src="https://user-images.githubusercontent.com/15327448/79340184-e8f44080-7f64-11ea-87f9-3ec90f9c5fa5.png" width="450" />

You need to organize the nodes by yourself.

## Multiple Edit

Open multiple assembly definition inspector views for parallel editing.

1. [Window] > [Asmdef Helper] > [Find all asmdef in project]
2. All asmdef will appear in project browser.
3. Pick up asmdef to edit.
4. [Window] > [Asmdef Helper] > [Open selected asmdef inspector view]
5. Open asmdef inspector views and edit asmdef.

<img src="https://user-images.githubusercontent.com/15327448/79342775-813ff480-7f68-11ea-851d-3e93b5948c0b.gif" width="450" />

## Sync Solution

Refresh .sln/.csproj in your project.

[Window] > [Asmdef Helper] > [Sync C# Solution]

I referred to this: [[SOLVED] Unity not generating .sln file from Assets > Open C# Project
](https://forum.unity.com/threads/solved-unity-not-generating-sln-file-from-assets-open-c-project.538487/)

___

## Environment
Unity 2019.3.4f1

This library use unity internal class, so may be not work (or couse build error) depending your unity vresion.

I referred to this: [【Unity, C#】internalな型やメンバにアクセスするには、多分これが一番早いと思います](https://qiita.com/mob-sakai/items/f3bbc0c45abc31ea7ac0)

## License
MIT

## Author
[@naninunenoy](https://github.com/naninunenoy)
