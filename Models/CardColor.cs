﻿using System;
using System.Collections.Generic;

namespace BlackJackMTG.Models;

public partial class CardColor
{
    public long CardId { get; set; }

    public long ColorId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Card Card { get; set; } = null!;

    public virtual Color Color { get; set; } = null!;
}
