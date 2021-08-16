using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Maat
{
    [SerializeField] private List<Mat> mats;

    public Mat getMat(MatPlace place)
    {  // Public because of ReendMaat.
        foreach (Mat mat in getMats())
        {
            if (mat.isPlace(place))
            {
                if (mat.getMaterial() == null) return null;
                return mat;
            }
        }
        return null;
    }
    private List<Mat> getMats()
    {
        if (mats == null) Debug.LogError("InspectorException");
        return mats;
    }
}
