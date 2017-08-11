using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class KeepUpdatingPlayerPostion : RAINAction
{
	private GameObject updatedPlayerLastSeen;

    public override void Start(RAIN.Core.AI ai)
    {
		updatedPlayerLastSeen = ai.WorkingMemory.GetItem<GameObject> ("UpdatedPlayerLastSeen");

        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
		updatedPlayerLastSeen.transform.position = ai.WorkingMemory.GetItem<GameObject> ("Player").transform.position;

		ai.WorkingMemory.SetItem<GameObject> ("PlayerLastSeen", ai.WorkingMemory.GetItem<GameObject> ("UpdatedPlayerLastSeen"));

		return ActionResult.RUNNING;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}