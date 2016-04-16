using UnityEngine;
using UnityEngine.Events;

public class ElectricalEvent : MonoBehaviour
{
    public ElectricalObject Input;
    public UnityEvent Event;

    public CallType WhenToCallEvent = CallType.PowerJustOn;

    bool lastOutput = false;

    void Update()
    {
        bool o = Input.IsOutputting();

        if (WhenToCallEvent == CallType.WithPower && o) {
            Event.Invoke();
        } else if (WhenToCallEvent == CallType.NoPower && !o) {
            Event.Invoke();
        } else if (lastOutput != o) {
            if (WhenToCallEvent == CallType.OnChange) {
                Event.Invoke();
            } else if (WhenToCallEvent == CallType.PowerJustOn && o) {
                Event.Invoke();
            } else if (WhenToCallEvent == CallType.PowerJustOff && !o) {
                Event.Invoke();
            }
        }
    }
}

public enum CallType
{
    OnChange,
    WithPower,
    NoPower,
    PowerJustOn,
    PowerJustOff
}