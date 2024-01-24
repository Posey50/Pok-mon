public interface IHealer
{
    /// <summary>
    /// Chooses a random K.O pokemon and resuscitate him with half of their HP.
    /// </summary>
    public void Revive();

    /// <summary>
    /// Chooses a random K.O pokemon and resuscitate him with all of their HP.
    /// </summary>
    public void MaxRevive();

    /// <summary>
    /// Heals all pokemons in the battle who are not K.O.
    /// </summary>
    public void Cook();

    /// <summary>
    /// Chooses a pokemon who's not K.O and give him all of their HP.
    /// </summary>
    public void FullHeal();

    /// <summary>
    /// Chooses to intervene or not and what to do if the healer intervenes.
    /// </summary>
    public void ChooseAnAction();
}