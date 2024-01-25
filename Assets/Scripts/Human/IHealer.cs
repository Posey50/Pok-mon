public interface IHealer
{
    /// <summary>
    /// Chooses a random KO pokemon and resuscitate him with half of their HP.
    /// </summary>
    public void Revive();

    /// <summary>
    /// Chooses a random KO pokemon and resuscitate him with all of their HP.
    /// </summary>
    public void MaxRevive();

    /// <summary>
    /// Heals all pokemons in the battle that are not KO.
    /// </summary>
    public void Cook();

    /// <summary>
    /// Chooses a pokemon that is not KO and gives him all of their HP.
    /// </summary>
    public void FullHeal();

    /// <summary>
    /// Chooses to intervene or not and what to do if the healer intervenes.
    /// </summary>
    public void ChooseAnAction();
}