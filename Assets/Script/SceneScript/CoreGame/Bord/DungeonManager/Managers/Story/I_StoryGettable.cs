using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_StoryGettable {
    public abstract IReadOnlyCollection<Lines> GetStoryList ();
    public abstract IReadOnlyCollection<Lines> GetProloge();
}
