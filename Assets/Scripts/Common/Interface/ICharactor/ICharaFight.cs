﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//战斗相关属性
public interface ICharaFight{
  
    //生命值
    int Hp { get; set; }

    void SetFightInfo(int hp);
}