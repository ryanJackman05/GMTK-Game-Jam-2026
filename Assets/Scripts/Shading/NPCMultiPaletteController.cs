using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPCSixPaletteController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock propBlock;

    [Header("Instance Colors")]
    public Color customColor1 = Color.blue;
    public Color customColor2 = Color.green;
    public Color customColor3 = Color.gray;
    public Color customColor4 = Color.white;
    public Color customColor5 = Color.black; // Targets the magenta parts of your sprite
    public Color customColor6 = Color.clear; // Targets the cyan parts of your sprite

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        propBlock = new MaterialPropertyBlock();
    }

    void Start()
    {
        ApplyPalette();
    }

    public void SetupNPC(Color c1, Color c2, Color c3, Color c4, Color c5, Color c6)
    {
        customColor1 = c1;
        customColor2 = c2;
        customColor3 = c3;
        customColor4 = c4;
        customColor5 = c5;
        customColor6 = c6;
        ApplyPalette();
    }

    private void ApplyPalette()
    {
        spriteRenderer.GetPropertyBlock(propBlock);
        
        propBlock.SetColor("_TargetColor1", customColor1);
        propBlock.SetColor("_TargetColor2", customColor2);
        propBlock.SetColor("_TargetColor3", customColor3);
        propBlock.SetColor("_TargetColor4", customColor4);
        propBlock.SetColor("_TargetColor5", customColor5); // Magenta mapping
        propBlock.SetColor("_TargetColor6", customColor6); // Cyan mapping
        
        spriteRenderer.SetPropertyBlock(propBlock);
    }
}