using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPCFourPaletteController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private MaterialPropertyBlock propBlock;

    [Header("Instance Colors")]
    public Color customColor1 = Color.blue;
    public Color customColor2 = Color.cyan;
    public Color customColor3 = Color.magenta;
    public Color customColor4 = Color.white; // Targets the yellow parts of your sprite

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        propBlock = new MaterialPropertyBlock();
    }

    void Start()
    {
        ApplyPalette();
    }

    public void SetupNPC(Color c1, Color c2, Color c3, Color c4)
    {
        customColor1 = c1;
        customColor2 = c2;
        customColor3 = c3;
        customColor4 = c4;
        ApplyPalette();
    }

    private void ApplyPalette()
    {
        spriteRenderer.GetPropertyBlock(propBlock);
        
        propBlock.SetColor("_TargetColor1", customColor1);
        propBlock.SetColor("_TargetColor2", customColor2);
        propBlock.SetColor("_TargetColor3", customColor3);
        propBlock.SetColor("_TargetColor4", customColor4); // Maps to the new slot
        
        spriteRenderer.SetPropertyBlock(propBlock);
    }
}