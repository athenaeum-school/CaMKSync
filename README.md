CaMKSync - LTP
=================

趣旨
--------------
CaMKSyncはUnityMolをベースにして

* 長期記憶領域のベースとなるプロテインキナーゼと神経細胞のconformationの手動調整
* bindingがMemory Switchとなりうる均衡状態の発見

を一般の生物学を知らない方でも挑戦できるゲームです。ゲームのカテゴリーとして市民科学ゲームにも分類が可能です。

但しゲームといっても、発見した分子構造や位相はIn Vitro(試験管)で再現ができる可能性があります。

conformationやプロテインへのバインディングはNP-Complete（完全ではないが）として知られており、
研究者の直観が、シミュレーション結果を上回る珍しい分野です。

本プロジェクトでは神経細胞由来のプロテイン（たんぱく質）のバインディングをコンピューター（つまり2進数、3進数の世界）
に見立てて、まず情報媒体として確立した後に

* たんぱく質での情報保存・記憶を再現するまでのシュミーレーション

を最終的な目標とし、脳がどのように働くかというメカニズムの解明ではなく、神経細胞の構成要素から記憶・演算領域を再現
することにより、人間の脳をモデルとした新たな脳の設計を考えるための基盤とするアプローチをとります。

最終的には

* たんぱく質や酵素を使ったハードディスク・演算処理装置の製造
* 生物にシグナルを送る
* 生物の行動パターンなどをプログラミングする

等の応用が考えられます。

バインディングの例
--------------

* https://www.youtube.com/watch?v=GLKhRBqHaBQ
* https://www.youtube.com/watch?v=_EvS2jCgdkU

参考資料
--------------
* http://ja.wikipedia.org/wiki/Foldit
* http://fold.it/portal/
* http://www.nature.com/nrn/journal/v3/n3/full/nrn753.html
* http://www.molecularbrain.com/content/6/1/10
* http://www.cell.com/biophysj/abstract/S0006-3495%2814%2900124-6
* http://www.plosbiology.org/article/info%3Adoi%2F10.1371%2Fjournal.pbio.0030124
* http://www.ploscompbiol.org/article/info%3Adoi%2F10.1371%2Fjournal.pcbi.1002421
* http://www.sciencedirect.com/science/article/pii/S0361923010002571

CaMKIIプロテインキナーゼとMicrotubuleのバインディングの例
--------------
http://www.newswise.com/articles/scientists-claim-brain-memory-code-cracked
![image](StrikingImage-press-release-450ppi-7in.png)


$Id: README 22 2009-10-21 21:37:11Z baaden $

This is UnityMol/SVN revision $Rev: 251 $.
This document is still work in progress.

UnityMol - Readme
=================

Scene organization
-------------------
* Molecule.unity: Full UnityMol application. Only works as standalone or in the editor.
* MainMenu.unity: Main Menu with buttons to load pre-defined scenes in a webplayer context.
* 1KX2.unity: pre-defined scene loading 1KX2.pdb from Scenes/1KX2/
* Fieldines.unity: pre-defined scene loading fieldline.pdb/.json/.obj from Scenes/fieldlines/
* Network.unity: pre-defined scene loading a CytoScape network
* FromPDB.unity: pre-defined scene fetching a PDB file corresponding to the PDB id entered in the Main Menu (no validation for the moment)

If a scene is not loaded correctly, no error message is displayed for the moment. Try to go back to the Main Menu (Open->Main menu) and load another scene.

Scene creation
--------------
- Let's create a scene called Toto:

	Copy Molecule.unity into Toto.unity
	Create a C# script named ScenePreload_Toto.cs in Assets/Scripts/. The class must inherit from MonoBehaviour.
	You need a method with the following signature:
		IEnumerator InitScene(RequestPDB requestPDB)
		{

		}
	Inspire yourself from ScenePreload_Fieldlines or ScenePreload_1KX2 to initialize the scene with the content you want.
	Be sure to be in the scene Toto in Unity and select the LoadBox object in the hierarchy.
	Then just add ScenePreload_Toto.cs as a component of LoadBox.

The method InitScene will be called after Molecule3D and the GUI are initialized.
Normally you can initialize everything and change any flag from here.
UIData.server_url will be www.shaman.ibpc.fr/umolweb in the Editor and "." otherwise.


About the HyperBalls shaders
----------------------------
The shaders are working great for OpenGL versions of Unity (MacOS, Windows with the -force-opengl flag).
If you want to modify these shaders and have them work in Direct3D you have to follow this procedure:

	Make your changes in the OpenGL version of the shaders: BallImproved.shader and StickImproved.shader.
	Let Unity compile them. It will throw some errors. Select the shader in the project tree and click on "Open compiled shader" in the inspector.
	Copy the content of this file and paste it into Ball_HyperBalls_D3D.shader or Stick_HyperBalls_D3D.shader respectively.
	Then replace the occurences of "oDepth.z" by "oDepth" . There is only one in the original shader.
	Change the name of the shader at the top of the file to "FvNano/Ball HyperBalls D3D" or "FvNano/Stick HyperBalls D3D".

UnityMol is able to detect the platform it runs on and choose the appropriate shaders.
