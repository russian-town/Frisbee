using UnityEngine;

public class SceneColorChanger : MonoBehaviour
{
    [SerializeField] private Color _groundColor;
    [SerializeField] private Color _frisbeeColor;
    [SerializeField] private Color _skyboxColor;
    [SerializeField] private Color _plateColor;
    [SerializeField] private Material _groundMaterial;
    [SerializeField] private Material _frisbeeMaterial;
    [SerializeField] private Material _plateMaterial;
    [SerializeField] private Camera _camera;

    private void Start()
    {
        _groundMaterial.color = _groundColor;
        _frisbeeMaterial.color = _frisbeeColor;
        _plateMaterial.color = _plateColor;
        _camera.backgroundColor = _skyboxColor;
    }
}
