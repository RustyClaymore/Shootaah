﻿public interface IGun
{
    bool IsShooting { get; set; }

    int Shot();

    bool IsReadyToShoot();
}