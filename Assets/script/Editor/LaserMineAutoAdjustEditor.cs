using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class LevelAutoAdjusterWindow : EditorWindow
{
    [MenuItem("Tools/Level Auto Adjuster")]
    public static void Open()
    {
        GetWindow<LevelAutoAdjusterWindow>("Level Auto Adjuster");
    }

    private void OnGUI()
    {
        GUILayout.Label("Auto Adjust Selected Objects", EditorStyles.boldLabel);

        if (GUILayout.Button("Adjust Selected"))
        {
            AdjustSelection();
        }
    }

    // ---------------- CORE ----------------

    private void AdjustSelection()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            // Adjust self
            TryAdjust(obj);

            // Adjust ONLY direct children
            foreach (Transform child in obj.transform)
            {
                TryAdjust(child.gameObject);
            }
        }
    }

    private void TryAdjust(GameObject obj)
    {
        if (obj.TryGetComponent(out Laser laser))
        {
            AdjustLaser(laser);
        }

        if (obj.TryGetComponent(out Landmine mine))
        {
            SnapMineToGround(mine);
        }
    }

    // ---------------- LASER ----------------

    private void AdjustLaser(Laser laser)
{
    Transform root = laser.transform;

    if (laser.leftCaster == null ||
        laser.rightCaster == null ||
        laser.beam == null)
    {
        Debug.LogError("Laser missing references", laser);
        return;
    }

    bool hitRight = Physics.Raycast(
        root.position,
        root.right,
        out RaycastHit rightHit,
        laser.maxCheckDistance
    );

    bool hitLeft = Physics.Raycast(
        root.position,
        -root.right,
        out RaycastHit leftHit,
        laser.maxCheckDistance
    );

    if (!hitRight || !hitLeft)
    {
        Debug.LogError("Laser could not find collision on X or -X", laser);
        return;
    }

    Undo.RecordObject(laser.leftCaster, "Adjust Laser");
    Undo.RecordObject(laser.rightCaster, "Adjust Laser");
    Undo.RecordObject(laser.beam, "Adjust Laser");

    // 1️⃣ Casters
    laser.rightCaster.position = rightHit.point;
    laser.leftCaster.position = leftHit.point;

    // 2️⃣ Beam midpoint
    Vector3 start = leftHit.point;
    Vector3 end = rightHit.point;
    Vector3 direction = (end - start).normalized;
    float distance = Vector3.Distance(start, end);

    laser.beam.position = (start + end) * 0.5f;
    

    // 3️⃣ Rotate beam so Y axis points along laser direction
    laser.beam.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    //laser.beamTrigger.rotation = Quaternion.FromToRotation(Vector3.up, direction);

    // 4️⃣ SCALE ON Y (CORRECT)
    Vector3 scale = laser.beam.localScale;
    scale.y = distance * 0.5f;   // IMPORTANT
    laser.beam.localScale = scale;
    
   

    Debug.Log($"Laser adjusted correctly: {laser.name}", laser);
}


    // ---------------- MINE ----------------

    private void SnapMineToGround(Landmine mine)
    {
        Transform t = mine.transform;

        // IMPORTANT FIX:
        // Always raycast from ABOVE the mine
        Vector3 rayStart = t.position + Vector3.up * 5f;

        if (Physics.Raycast(
            rayStart,
            Vector3.down,
            out RaycastHit hit,
            20f,
            Physics.DefaultRaycastLayers,
            QueryTriggerInteraction.Ignore))
        {
            Undo.RecordObject(t, "Snap Mine To Ground");

            // FIX: place ON ground, not add offset repeatedly
            t.position = hit.point + Vector3.up * mine.groundOffset;

            Debug.Log($"Mine snapped to ground: {mine.name}", mine);
        }
        else
        {
            Debug.LogError($"Mine could not find ground: {mine.name}", mine);
        }
    }
}
