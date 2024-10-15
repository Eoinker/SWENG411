using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFlags : MonoBehaviour
{

    [Header("Ground Detection")]
    [SerializeField] private LayerMask groundLayer;
    private bool isGrounded;

    // Update is called once per frame
    void Update()
    {
        UpdateGrounded();
    }


    #region Ground Detection

    public bool IsGrounded()
    {
        return isGrounded;
    }

    private void UpdateGrounded()
    {
        
    }

    #endregion
}
