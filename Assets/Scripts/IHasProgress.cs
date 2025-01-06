using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{

    // Bu arayüzde tanýmlanan bir olaydýr. Ýlerleme durumu deðiþtiðinde tetiklenir.
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;

    // Ýlerleme durumunun güncellendiðini bildiren sýnýf
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progressNormalized;  // Ýlerleme yüzdesi, 0 ile 1 arasýnda bir deðer
    }

}
