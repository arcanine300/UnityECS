using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Extensions;
using Unity.Jobs;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gameObjPrefabs;
    private Entity[] entityPrefabs;
    [SerializeField]
    private int entCount = 5000;

    //private Entity[] entArr;
    private EntityManager eMan;

    // Start is called before the first frame update
    private void Start()
    {
        entityPrefabs = new Entity[gameObjPrefabs.Length];
        //entArr = new Entity[entCount];

        World theWorld = World.DefaultGameObjectInjectionWorld;
        eMan = theWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(theWorld, null);

        for (int i = 0; i < gameObjPrefabs.Length; i++)
        {
            entityPrefabs[i] = GameObjectConversionUtility.ConvertGameObjectHierarchy(gameObjPrefabs[i], settings);
        }

        
        InstantiateEntities();
        //MakeEntity();
    }
    private void InstantiateEntities()
    {
        float x = 0f;
        float y = 0f;
        float z = 0f;

        for (int i = 0; i < entCount; i++)
        {
            //instantiate entity prefab
            Entity ent = eMan.Instantiate(entityPrefabs[UnityEngine.Random.Range(0, entityPrefabs.Length)]);

            x = UnityEngine.Random.Range(-500f, 500f);
            y = UnityEngine.Random.Range(-500f, 500f);
            z = UnityEngine.Random.Range(-500f, 500f);

            eMan.SetComponentData(ent, new Translation { 
                Value = new float3(x,y,z)
            });

        }
    }
    /*
    private void MakeEntity()
    {
        //create new entity type and assign data to this entity, data passed in is for rendering the obj and positioning
        EntityArchetype entityType1 = eMan.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld)
            );

        float x = 0f;
        float y = 0f;
        float z = 0f;

        for (int i = 0; i < entCount; i++)
        {
            //create an entity in the world of archetpe entityType1, created in the entity debugger
            entArr[i] = eMan.CreateEntity(entityType1);

            x = UnityEngine.Random.Range(-250f, 250f);  
            y = UnityEngine.Random.Range(-250f, 250f);
            z = UnityEngine.Random.Range(-250f, 250f);

            eMan.AddComponentData(entArr[i], new Translation
            {
                Value = new float3(x, y, z)
            });

            eMan.AddSharedComponentData(entArr[i], new RenderMesh
            {
                mesh = entityMesh,
                material = entityMats[UnityEngine.Random.Range(0, entityMats.Length)]
            });
        }

    }
    */
}