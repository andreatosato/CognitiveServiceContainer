﻿using ARMWebApp.Entities;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ARMWebApp.Services
{
    public interface ICustomVision
    {
        Task<CustomVisionResponse> FromUrlImage(Uri urlImage);
        Task<CustomVisionResponse> FromByteArrayImage(Stream payloadImage);
    }
}
