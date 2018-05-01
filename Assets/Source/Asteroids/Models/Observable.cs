using System;

public class Observable<T>
{
    public Action<T> OnUpdated;
    
    public T Value
    {
        get
        {
            return _value;
        }

        set
        {
            _value = value;
            if (OnUpdated != null)
            {
                OnUpdated(_value);
            }
        }
    }

    private T _value;
}