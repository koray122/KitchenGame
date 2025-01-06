using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{

    // Bu aray�zde tan�mlanan bir olayd�r. �lerleme durumu de�i�ti�inde tetiklenir.
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    // �lerleme durumunun g�ncellendi�ini bildiren s�n�f
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;  // �lerleme y�zdesi, 0 ile 1 aras�nda bir de�er
    }

}
