using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Damageable _tracking;
    
    // Start is called before the first frame update
    void Start()
    {   
        _tracking = GetComponent<Damageable>(); // find the damagable thing to to track
        _tracking.bar = this;
    }
    
  
    public void updateBar() {
        // update the health bar
    }
}
