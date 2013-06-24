#pragma once
#include "Stdafx.h"
#include "Ligature.h"

using namespace System;
using namespace System::Collections::Generic;

namespace Dzonny { namespace XmlKeyboard { namespace Interop {

    /// <summary>Initializes managed array from unmanaged array (pointers) until null pointer is reacched</summary>
    /// <typeparam name="TUnmanaged">Type of items in unmanaged array</typeparam>
    /// <typeparam name="TManaged">Type of items in managed array</typeparam>
    /// <param name="pointer">Pointer to pointer to first item</param>
    /// <returns>Array of managed objects initialized by unmanaged objects</returns>
    /// <exception cref="ArgumentNullException"><paramref name="pointer"/> is null</exception>
    template <typename TUnmanaged, typename TManaged>
    cli::array<TManaged^>^ InitArrayUntilNull(TUnmanaged** pointer){
        if(pointer == NULL) throw gcnew ArgumentNullException("pointer");
        auto ret = gcnew List<TManaged^>();
        for(TUnmanaged** ptr = pointer; *ptr != NULL; ptr++){
            ret->Add(gcnew TManaged(*ptr));
        }
        return ret->ToArray();
    }

    /// <summary>Initializes managed array from unmanaged array (pointers) of defined length</summary>
    /// <typeparam name="TUnmanaged">Type of items in unmanaged array</typeparam>
    /// <typeparam name="TManaged">Type of items in managed array</typeparam>
    /// <param name="pointer">Pointer to pointer to first item</param>
    /// <param name="count">Number of items in unmanaged array</param>
    /// <returns>Array of managed objects initialized by unmanaged objects</returns>
    template <typename TUnmanaged, typename TManaged>
    cli::array<TManaged^>^ InitArrayCount(TUnmanaged* pointer, int count){
        auto ret = gcnew List<TManaged^>(count);
        for(int i = 0; i < count; i++){
            ret->Add(gcnew TManaged(&pointer[i]));
        }
        return ret->ToArray();
    }

    /// <summary>Initializes managed array from unmanaged array (pointers) of defined length when managed and unmanaged types are same</summary>
    /// <typeparam name="T">Type of items in both - unmanaged and managed array</typeparam>
    /// <param name="pointer">Pointer to pointer to first item</param>
    /// <param name="count">Number of items in unmanaged array</param>
    /// <returns>Array of managed objects initialized by unmanaged objects</returns>
    template <typename T>
    cli::array<T>^ InitArrayCount(T* pointer, int count){
        auto ret = gcnew List<T>(count);
        for(int i = 0; i < count; i++){
            ret->Add(pointer[i]);
        }
        return ret->ToArray();
    }

    /// <summary>Initializes managed array from unmanaged array (pointers) until zero element is reached</summary>
    /// <typeparam name="TUnmanaged">Type of items in unmanaged array</typeparam>
    /// <typeparam name="TManaged">Type of items in managed array</typeparam>
    /// <param name="pointer">Pointer to pointer to first item</param>
    /// <returns>Array of managed objects initialized by unmanaged objects</returns>
    /// <remarks>Specialized template instance of <see cref="IsZero&lt;T>"/> for type <typeparamref name="TUnmanaged"/> must be defined for this function for work</remarks>
    template <typename TUnmanaged, typename TManaged>
    cli::array<TManaged^>^ InitArrayUntilZero(TUnmanaged* pointer){
        List<TManaged^>^ ret = gcnew List<TManaged^>();
        for(auto ptr = pointer; !IsZero<TUnmanaged>(ptr); ptr++){
            ret->Add(gcnew TManaged(ptr));
        }
        return ret->ToArray();
    }

    /// <summary>Initializes managed array from unmanaged array (pointers) until zero element is reached, passing additional parameter to newly created managed object</summary>
    /// <typeparam name="TUnmanaged">Type of items in unmanaged array</typeparam>
    /// <typeparam name="TManaged">Type of items in managed array</typeparam>
    /// <typeparam name="TP">Type of object to be passed to managed type</typeparam>
    /// <param name="pointer">Pointer to pointer to first item</param>
    /// <param name="p">A parameter to be passed to managed object</param>
    /// <returns>Array of managed objects initialized by unmanaged objects</returns>
    /// <remarks>Specialized template instance of <see cref="IsZero&lt;T>"/> for type <typeparamref name="TUnmanaged"/> must be defined for this function for work</remarks>
    template <typename TUnmanaged, typename TManaged, typename TP>
    cli::array<TManaged^>^ InitArrayUntilZero(TUnmanaged* pointer, TP p){
        List<TManaged^>^ ret = gcnew List<TManaged^>();
        for(auto ptr = pointer; !IsZero<TUnmanaged>(ptr); ptr++){
            ret->Add(gcnew TManaged(ptr, p));
        }
        return ret->ToArray();
    }

    /// <summary>
    /// Initializes managed array from unmanaged array (pointers) until zero element is reached.
    /// Passes additional parameter to newly created managed object.
    /// Allows size of unmanaged object to bi different then size of type passed.
    /// </summary>
    /// <typeparam name="TUnmanaged">Type of items in unmanaged array</typeparam>
    /// <typeparam name="TManaged">Type of items in managed array</typeparam>
    /// <typeparam name="TP">Type of object to be passed to managed type</typeparam>
    /// <param name="pointer">Pointer to pointer to first item</param>
    /// <param name="pointerSize">Size (in bytes) of each object in array pointed by <paramref name="pointer"/></param>
    /// <param name="p">A parameter to be passed to managed object</param>
    /// <returns>Array of managed objects initialized by unmanaged objects</returns>
    /// <remarks>Specialized template instance of <see cref="IsZero&lt;T>"/> for type <typeparamref name="TUnmanaged"/> must be defined for this function for work</remarks>
    template <typename TUnmanaged, typename TManaged, typename TP>
    cli::array<TManaged^>^ InitArrayUntilZero(TUnmanaged* pointer, BYTE pointerSize, TP p){
        List<TManaged^>^ ret = gcnew List<TManaged^>();
        for(int i = 0; !IsZero<TUnmanaged>((TUnmanaged*)((BYTE*)pointer + i * pointerSize)); i++){
            ret->Add(gcnew TManaged((TUnmanaged*)((BYTE*)pointer + i * pointerSize), p));
        }
        return ret->ToArray();
    }

#pragma region IsZero
    /// <summary>When implemented for given type <typeparamref name="T"/> gets value indicating if instance of that type represents zero value</summary>
    /// <typeparam name="T">Type of item to detect if it is zero</typeparam>
    /// <param name="item">Item to test if it is zero</param>
    /// <returns>True if <paramref name="item"/> represents zero item; false otherwise</returns>
    /// <remarks>There is no generic implementation of this template function. A specialzed implementation must be provided for each type</remarks>
    template <typename T>
    bool IsZero(T* item) = 0;

    /// <summary>Specialized implementation of <see cref="IsZero&lt;T>"/> for <see cref="VK_TO_WCHAR_TABLE"/></summary>
    /// <param name="item">Item to test if it is zero</param>
    /// <returns>True if <paramref name="item"/> is null or represents zero item; false otherwise</returns>
    template <>
    bool IsZero<VK_TO_WCHAR_TABLE>(PVK_TO_WCHAR_TABLE item);

    /// <summary>Specialized implementation of <see cref="IsZero&lt;T>"/> for <see cref="DEADKEY"/></summary>
    /// <param name="item">Item to test if it is zero</param>
    /// <returns>True if <paramref name="item"/> is null or represents zero item; false otherwise</returns>
    template <>
    bool IsZero<DEADKEY>(PDEADKEY item);

    /// <summary>Specialized implementation of <see cref="IsZero&lt;T>"/> for <see cref="VSC_LPWSTR"/></summary>
    /// <param name="item">Item to test if it is zero</param>
    /// <returns>True if <paramref name="item"/> is null or represents zero item; false otherwise</returns>
    template <>
    bool IsZero<VSC_LPWSTR>(PVSC_LPWSTR item);

    /// <summary>Specialized implementation of <see cref="IsZero&lt;T>"/> for <see cref="VSC_VK"/></summary>
    /// <param name="item">Item to test if it is zero</param>
    /// <returns>True if <paramref name="item"/> is null or represents zero item; false otherwise</returns>
    template<>
    bool IsZero<VSC_VK>(PVSC_VK item);

    /// <summary>Specialized implementation of <see cref="IsZero&lt;T>"/> for <see cref="LIGATURE1"/></summary>
    /// <param name="item">Item to test if it is zero. Can be pointer to <see cref="LIGATURE1"/>, <see cref="LIGATURE2"/>, <see cref="LIGATURE3"/>, <see cref="LIGATURE4"/> or <see cref="LIGATURE5"/></param>
    /// <returns>True if <paramref name="item"/> is null or represents zero item; false otherwise</returns>
    template<>
    bool IsZero<LIGATURE1>(PLIGATURE1 item);

    /// <summary>Specialized implementation of <see cref="IsZero&lt;T>"/> for <see cref="VK_TO_BIT"/></summary>
    /// <param name="item">Item to test if it is zero.</param>
    /// <returns>True if <paramref name="item"/> is null or represents zero item; false otherwise</returns>
    template<>
    bool IsZero<VK_TO_BIT>(PVK_TO_BIT item);

    /// <summary>Specialized implementation of <see cref="IsZero&lt;T>"/> for <see cref="VK_TO_WCHARS1"/></summary>
    /// <param name="item">Item to test if it is zero.</param>
    /// <returns>True if <paramref name="item"/> is null or represents zero item; false otherwise</returns>
    template<>
    bool IsZero<VK_TO_WCHARS1>(PVK_TO_WCHARS1 item);
#pragma endregion

    /// <summary>Initializes managed array of <see cref="Ligature"/> from unmanaged array (pointers) of <see cref="LIGATURE1"/> until zero element is reached</summary>
    /// <param name="pointer">Pointer to pointer to first item of type <see cref="LIGATURE1"/>, <see cref="LIGATURE2"/>, <see cref="LIGATURE3"/>, <see cref="LIGATURE4"/>, or <see cref="LIGATURE5"/>. Depends on <paramref name="ligatureMax"/> and <paramref name="ligatureLength"/>.</param>
    /// <param name="ligatureMax">Maximum lenght of ligature, in characters</param>
    /// <param name="ligatureLength">Lenght of structure pointer to is passed to <paramref name="pointer"/>.</param>
    /// <returns>Array of managed <see cref="Ligature"/> objects initialized by unmanaged <see cref="LIGATURE1"/> objects</returns>
    /// <remarks><paramref name="ligatureMax"/> and <paramref name="ligatureLength"/> must have corresponding values. They are not verified by this function.</remarks>
    cli::array<Ligature^>^ InitArrayUntilZero(PLIGATURE1 pointer, BYTE ligatureMax, BYTE ligatureLength);
}}}