// From : https://dev.to/shimmer/the-curry-howard-correspondence-3obh

using System;

public class CurryHowardCorrespondence
{
    // ((A → B) ∧ A) → B
    private static B ModusPonens<A, B>(
        Func<A, B> A_implies_B,
        A proof_of_A)
    {
        B proof_of_B = A_implies_B(proof_of_A);
        return proof_of_B;
    }

    // ((A → B) ∧ (B → C)) → (A → C)
    private Func<A, C> Syllogism<A, B, C>(
        Func<A, B> A_implies_B,
        Func<B, C> B_implies_C)
    {
        C Composed(A a)
        {
            return B_implies_C(A_implies_B(a));
        }
        return Composed;
    }

    public enum Absurd
    {
    }

    // ¬A
    public class Not<A>
    {
        public Not(Func<A, Absurd> A_implies_absurd)
        {
            _func = A_implies_absurd;
        }

        public Absurd Apply(A proof_of_A)
            => _func(proof_of_A);

        private readonly Func<A, Absurd> _func;
    }

    // ((A → B) ∧ ¬B) → ¬A
    public Not<A> ModusTollens<A, B>(
        Func<A, B> A_implies_B,
        Not<B> not_B)
    {
        Absurd A_to_absurb(A a)
        {
            return not_B.Apply(A_implies_B(a));
        }
        return new Not<A>(A_to_absurb);
    }

    // A → ¬¬A
    public static Not<Not<A>> CreateDoubleNegative<A>(A proof_of_A)
    {
        Absurd not_A_implies_absurd(Not<A> not_A)
            => not_A.Apply(proof_of_A);
        return new Not<Not<A>>(not_A_implies_absurd);
    }

    public static void Main(string[] args)
    {
        Console.WriteLine ("Curry-Howard Correspondence in C#");
    }
}