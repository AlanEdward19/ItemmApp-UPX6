﻿using ItemmApp.Models.Request;
using ItemmApp.Models.Response;

namespace ItemmApp.Interfaces;

public interface IClassRepository
{
    Task<IEnumerable<ClassResponse>> GetClassesAsync();
}