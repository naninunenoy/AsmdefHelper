# Asmdef Helper

<img src="https://user-images.githubusercontent.com/15327448/79349410-24950780-7f71-11ea-94be-056726828ec4.png" width="140" />

Unity assembly definition utilities.

This library solve inconvenience of assembly definition on unity.

## Dependency Graph

Unity assembly definition viewer.

(menu) > [AsmdefHelper] > [Open DependencyGraph]

Show assembly definition referances in your project like this.

<img src="https://user-images.githubusercontent.com/15327448/79340184-e8f44080-7f64-11ea-87f9-3ec90f9c5fa5.png" width="450" />

You need to organize the nodes by yourself.

## Multiple Edit

Open multiple assembly definition inspector views for parallel editing.

1. [AsmdefHelper] > [Find all asmdef in project]
2. All asmdef will appear in project browser.
3. Pick up asmdef to edit.
4. [AsmdefHelper] > [Open selected asmdef inspector view]
5. Open asmdef inspector views and edit these asmdef.

<img src="https://user-images.githubusercontent.com/15327448/79342775-813ff480-7f68-11ea-851d-3e93b5948c0b.gif" width="450" />

## Sync Solution

Refresh .sln/.csproj in your project.

(menu) > [AsmdefHelper] > [Sync C# Solution]

I referred to this: [[SOLVED] Unity not generating .sln file from Assets > Open C# Project
](https://forum.unity.com/threads/solved-unity-not-generating-sln-file-from-assets-open-c-project.538487/)

## Compile Locker

Lock unity editor compile to edit an asmdef.

(menu) > [AsmdefHelper] > [Compile Lock]
 * When checked, unity editor compile is stop.

I referred to this: [decoc/CompileLocker.cs](https://gist.github.com/decoc/bde047ac7ad8c9bfce7eb408f2712424)


## Custom Create
Create an asmdef with some parametors you input.

 * (right mouse button click) > [AsmdefHelper] > [create custom asmdef]
 * input your parameters.
 * click [Create] button.

<img src="https://user-images.githubusercontent.com/15327448/95679389-77dccf80-0c0d-11eb-9032-5e60024b7c74.gif" width="450" />

When you checked `Is Editor`, to be created an asmdef for only platform **Editor**.

I referred to this: [【Unity】Assembly Definition を作成する時のコンパイル回数を抑えられるエディタ拡張「UniAssemblyDefinitionCreator」を GitHub に公開しました](https://baba-s.hatenablog.com/entry/2020/09/11/090000)

___

## Environment
Unity 2020.1.6f1

This library use unity internal class, so may be not work (or couse build error) depending your unity vresion.

I referred to this: [【Unity, C#】internalな型やメンバにアクセスするには、多分これが一番早いと思います](https://qiita.com/mob-sakai/items/f3bbc0c45abc31ea7ac0)

## License
MIT

## Author
[@naninunenoy](https://github.com/naninunenoy)
