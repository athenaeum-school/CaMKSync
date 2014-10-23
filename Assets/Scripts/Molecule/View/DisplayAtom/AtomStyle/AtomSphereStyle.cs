/// @file AtomSphereStyle.cs
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
/// $Id: AtomSphereStyle.cs 213 2013-04-06 21:13:42Z baaden $
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


namespace Molecule.View.DisplayAtom
{

	using UnityEngine;
	using System.Collections;
	using Molecule.Model;
	using Molecule.Control;
	using UI;
	public class AtomSphereStyle : IAtomStyle
	{
		public static ArrayList atomOrderList=new ArrayList();

		private UIData.AtomType atomtype;

		public static GameObject AtomCubeParent = new GameObject("AtomCubeParent");

		public AtomSphereStyle()	
		{
			
		}


		public void DisplayAtoms(UIData.AtomType type_atom, bool force_display = false)
		{
			if(type_atom != UIData.AtomType.particleball || !UIData.isParticlesInitialized ||force_display)
			{
				if(MoleculeModel.atoms != null)
				{
					MoleculeModel.atoms.Clear();
					MoleculeModel.atoms=null;
				}
				MoleculeModel.atoms=new ArrayList();

				atomtype = type_atom;

				if(atomtype==UIData.AtomType.sphere)
				{
					if(UIData.secondarystruct)
						DisplayAtomMethodBySphere(MoleculeModel.CaSplineList, MoleculeModel.CaSplineTypeList);
					else
						DisplayAtomMethodBySphere(MoleculeModel.atomsLocationlist, MoleculeModel.atomsTypelist);
				}
			}
		}
		
		public void DisplayAtomMethodBySphere(ArrayList alist, ArrayList typelist)
		{
			Vector3 vl=new Vector3();
			MoleculeModel.atomsnumber=alist.Count;

			for(int i=0;i<alist.Count;i++)
			{
				
				GameObject o;
				
				float [] a=alist[i] as float[];
				AtomModel aModel= (typelist[i] as AtomModel);
				vl.x=a[0];
				vl.y=a[1];
				vl.z=a[2];
				BallUpdateSphere comp1;

				// o=(GameObject)MonoBehaviour.Instantiate(Resources.Load(aModel.type),vl,new Quaternion(0f,0f,0f,0f));
				o = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				o.transform.position = vl;
				MoleculeModel.atoms.Add(o);
				o.transform.parent = AtomCubeParent.transform;
				comp1 = o.AddComponent<BallUpdateSphere>();
				comp1.rayon = aModel.radius*2;
				comp1.number=i;
				comp1.enabled = true;
				o.transform.renderer.materials[0].SetColor("_Color", aModel.baseColor);
				comp1.SetRayonFactor(aModel.scale/100);
				o.tag=aModel.type;
			}
		}


	}
}
