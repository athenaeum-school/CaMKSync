/// @file Clip4Billboard.cs
/// @brief Details to be specified
/// @author FvNano/LBT team
/// @author Marc Baaden <baaden@smplinux.de>
/// @date   2013-4
///
/// Copyright Centre National de la Recherche Scientifique (CNRS)
///
/// contributors :
/// FvNano/LBT team, 2010-13
/// Marc Baaden, 2010-13
///
/// baaden@smplinux.de
/// http://www.baaden.ibpc.fr
///
/// This software is a computer program based on the Unity3D game engine.
/// It is part of UnityMol, a general framework whose purpose is to provide
/// a prototype for developing molecular graphics and scientific
/// visualisation applications. More details about UnityMol are provided at
/// the following URL: "http://unitymol.sourceforge.net". Parts of this
/// source code are heavily inspired from the advice provided on the Unity3D
/// forums and the Internet.
///
/// This software is governed by the CeCILL-C license under French law and
/// abiding by the rules of distribution of free software. You can use,
/// modify and/or redistribute the software under the terms of the CeCILL-C
/// license as circulated by CEA, CNRS and INRIA at the following URL:
/// "http://www.cecill.info".
/// 
/// As a counterpart to the access to the source code and rights to copy, 
/// modify and redistribute granted by the license, users are provided only 
/// with a limited warranty and the software's author, the holder of the 
/// economic rights, and the successive licensors have only limited 
/// liability.
///
/// In this respect, the user's attention is drawn to the risks associated 
/// with loading, using, modifying and/or developing or reproducing the 
/// software by the user in light of its specific status of free software, 
/// that may mean that it is complicated to manipulate, and that also 
/// therefore means that it is reserved for developers and experienced 
/// professionals having in-depth computer knowledge. Users are therefore 
/// encouraged to load and test the software's suitability as regards their 
/// requirements in conditions enabling the security of their systems and/or 
/// data to be ensured and, more generally, to use and operate it in the 
/// same conditions as regards security.
///
/// The fact that you are presently reading this means that you have had 
/// knowledge of the CeCILL-C license and that you accept its terms.
///
/// $Id: Clip4Billboard.cs 213 2013-04-06 21:13:42Z baaden $
///
/// References : 
/// If you use this code, please cite the following reference : 	
/// Z. Lv, A. Tek, F. Da Silva, C. Empereur-mot, M. Chavent and M. Baaden:
/// "Game on, Science - how video game technology may help biologists tackle
/// visualization challenges" (2013), PLoS ONE 8(3):e57990.
/// doi:10.1371/journal.pone.0057990
///
/// If you use the HyperBalls visualization metaphor, please also cite the
/// following reference : M. Chavent, A. Vanel, A. Tek, B. Levy, S. Robert,
/// B. Raffin and M. Baaden: "GPU-accelerated atom and dynamic bond visualization
/// using HyperBalls, a unified algorithm for balls, sticks and hyperboloids",
/// J. Comput. Chem., 2011, 32, 2924
///

using UnityEngine;
using System.Collections;

public class Clip4Billboard : MonoBehaviour {

	// Use this for initialization

	public static GameObject  CreateClip (){

GameObject obj=new GameObject("Clip");

obj.AddComponent<SkinnedMeshRenderer>();

SkinnedMeshRenderer renderer_= obj.GetComponent<SkinnedMeshRenderer>();

// Build basic mesh_

Mesh _mesh= new Mesh();

//_mesh.vertices = new Vector3[] {new Vector3(-1f, -1f, 0), new Vector3(1f, -1f, 0), new Vector3(-1f, 1f, 0),new Vector3(1f, 1f, 0)};
_mesh.vertices = new Vector3[] {new Vector3(-0.5f, -0.5f, 0), new Vector3(0.5f, -0.5f, 0), new Vector3(-0.5f, 0.5f, 0),new Vector3(0.5f, 0.5f, 0)};

_mesh.uv = new Vector2[] {new Vector2(0, 0), new Vector2(1, 0), new Vector2(0, 1), new Vector2(1, 1)};

_mesh.triangles = new int[] {0, 1, 2, 1, 3, 2};

_mesh.RecalculateNormals();

renderer_.material = new Material (Shader.Find(" Diffuse"));

 

// Assign bone weights to mesh_

// We use 2 bones. One for the lower vertices, one for the upper vertices.

BoneWeight[] weights= new BoneWeight[4];

 

weights[0].boneIndex0 = 0;

weights[0].weight0 = 1;

 

weights[1].boneIndex0 = 0;

weights[1].weight0 = 1;

 

weights[2].boneIndex0 = 1;

weights[2].weight0 = 1;

 

weights[3].boneIndex0 = 1;

weights[3].weight0 = 1;

 

_mesh.boneWeights = weights;

// Create Bone Transforms and Bind poses

// One bone at the bottom and one at the top

Transform[] bones= new Transform[2];

Matrix4x4[] bindPoses= new Matrix4x4[2];

 

bones[0] = new GameObject ("Lower").transform;

bones[0].parent = obj.transform;

// Set the position relative to the parent

bones[0].localRotation = Quaternion.identity;

bones[0].localPosition = new Vector3 (0, -0.5f, 0);
//bones[0].localPosition = new Vector3 (0, -1f, 0);

// The bind pose is bone's inverse transformation matrix

// In this case the matrix we also make this matrix relative to the root

// So that we can move the root game object around freely

bindPoses[0] = bones[0].worldToLocalMatrix * obj.transform.localToWorldMatrix;

bones[1] = new GameObject ("Upper").transform;

bones[1].parent = obj.transform;

// Set the position relative to the parent

bones[1].localRotation = Quaternion.identity;

bones[1].localPosition = new Vector3 (0, 0.5f, 0);
//bones[1].localPosition = new Vector3 (0, 1f, 0);
// The bind pose is bone's inverse transformation matrix

// In this case the matrix we also make this matrix relative to the root

// So that we can move the root game object around freely

bindPoses[1] = bones[1].worldToLocalMatrix * obj.transform.localToWorldMatrix;

 

_mesh.bindposes = bindPoses;

// Assign bones and bind poses

renderer_.bones = bones;

renderer_.sharedMesh = _mesh;

 
return obj;
}
}
