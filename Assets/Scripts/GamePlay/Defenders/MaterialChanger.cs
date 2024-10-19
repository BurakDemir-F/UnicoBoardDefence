using System.Collections.Generic;
using UnityEngine;

namespace Defenders
{
    public class MaterialChanger : MonoBehaviour,IMaterialChanger
    {
        [SerializeField] private List<MeshRenderer> _meshRenderers;
        [SerializeField] private Material _transparentMat; 
        [SerializeField] private Material _opaqueMat;

        public void MakeTransparent()
        {
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.material = _transparentMat;
            }
        }

        public void MakeOpaque()
        {
            foreach (var meshRenderer in _meshRenderers)
            {
                meshRenderer.material = _opaqueMat;
            }
        }
    }

    public interface IMaterialChanger
    {
        void MakeTransparent();
        void MakeOpaque();
    }
}