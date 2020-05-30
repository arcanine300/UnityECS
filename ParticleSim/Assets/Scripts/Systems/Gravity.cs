using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Collections;

public class Gravity : SystemBase
{
    protected override void OnUpdate()
    {
        float y = (float)Time.DeltaTime * -9.81f;

        Entities.ForEach((ref Translation pos) =>
        {
            if (pos.Value.y <= -500f)
            {
                pos.Value.y = 500f;
            }
            pos.Value = new float3(pos.Value.x, pos.Value.y + y, pos.Value.z);
        })
        .ScheduleParallel();
    }
}

//old way to multithread
/*
 * public class Gravity : JobComponentSystem  
{
    protected override JobHandle OnUpdate(JobHandle inputDependencies)
    {
        float y = (float)Time.DeltaTime * -9.81f;

        var jobHandle = Entities.ForEach((ref Translation pos) =>
        {
            pos.Value = new float3(pos.Value.x, pos.Value.y + y, pos.Value.z);
        })
        .Schedule(inputDependencies);

        return jobHandle;
    }
}
 */

