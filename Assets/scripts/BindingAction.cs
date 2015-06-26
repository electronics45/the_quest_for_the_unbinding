using UnityEngine;
using System.Collections;

public interface BindingAction
{
	void executeBinding (string actionName);
	
	void onBindingRelease (string actionName);
}
