﻿using System;
using System.Collections.Generic;

namespace hobd
{

/// <summary>Accumulator is an aggregated sensor, which value is known to be constantly increasing</summary>
/// Accumulator differs from aggregator in the way it takes history data.
/// Accumulator is a constantly increasing value, which is resetted manually (or by autoevent)
/// Intermediate values are of no interest to accumulators. Only last actual value is important
public interface IAccumulatorSensor : IAggregatorSensor
{

}

}
