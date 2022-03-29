using System.Collections;
using System.Collections.Generic;

public static class Utility
{   
    //The Fisher-Yates Shuffle
    public static T[] ShuffleArray<T>(T[] array, int seed)
    {
        System.Random prng = new System.Random(seed); // call the random generator

        for(int i = 0; i < array.Length - 1; i++)
        {
            int randomIndex = prng.Next(i, array.Length); // random generate a integer between i and array length(exclusive)

            // Swap the elements
            T tempItem = array[randomIndex];
            array[randomIndex] = array[i];
            array[i] = tempItem;
        }

        return array;
    }
}

