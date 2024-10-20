using System;
using UnityEngine;

namespace GamePlay.Areas
{
    public class GameArea : AreaBase
    {
        private IAreaIndicator _areaIndicator;
        public IAreaIndicator AreaIndicator => _areaIndicator;
        
        public override void OnGetFromPool()
        {
            base.OnGetFromPool();
            _areaIndicator = GetComponent<IAreaIndicator>();
        }

        public override void OnReturnedToPool()
        {
            base.OnReturnedToPool();
            _areaIndicator.DeactivateIndicator();   
        }

        private bool _isGizmoDrawing;
        public void DrawGizmo()
        {
            _isGizmoDrawing = true;
        }

        public void CloseGizmo()
        {
            _isGizmoDrawing = false;
        }

        private void OnDrawGizmos()
        {
            if(!_isGizmoDrawing)
                return;
            
            Gizmos.DrawCube(CenterPosition,Vector3.one);
        }
    }
}