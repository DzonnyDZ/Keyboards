/// <summary>Initializes a managed array based on unmanaged pointer</summary>
/// <param name="target">Target of array value (managed array)</param>
/// <param name="source">Source of array value (unmanaged pointer)</param>
/// <param name="type">Type of managed array elements</param>
/// <param name="init">An expression to initialize <paramref name="target"/> when <paramref name="source"/> is not null.</param>
#define INITARRAY(target, source, type, init) \
    if(target == nullptr){ \
        if(source == NULL){ \
            target = gcnew cli::array<type>(0); \
        } else { \
            target = init; \
        } \
    }

///// <summary>Determines if an object is instance of type</summary>
///// <param name="object">Object to test</object>
///// <param name="type">Managed type to test if <paramref name="object"/> is instance of</param>
///// <returns>Boolean value endicating if <paramref name="object"/> is of type <paramref name="type"/></returns>
//#define ISINST(object, type) (cli::dynamic_cast<type>(object) != nullptr)