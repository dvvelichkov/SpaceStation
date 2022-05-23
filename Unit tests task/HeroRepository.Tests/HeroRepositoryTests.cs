using System;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class HeroRepositoryTests
{
    private string name;
    private int level;
    private Hero hero;
    private List<Hero> data;
    private HeroRepository heroRepository;

    [SetUp]

    public void SetUp()
    {
        this.name = "Ivan";
        this.level = 5;
        this.hero = new Hero(this.name, this.level);
        this.data = new List<Hero>();
        this.heroRepository = new HeroRepository();
    }

    [Test]
    public void WhenHeroGetsInstatiated_ShouldBeSetCorrectly()
    {
        Hero hero = new Hero("Pesho", 8);
        Assert.AreEqual(hero.Name, "Pesho");
        Assert.AreEqual(hero.Level, 8);
    }

    [Test]
    public void WhenHeroRepositoryCreatesHero_NullHeroShouldThrowException()
    {
        Hero hero = null;
        HeroRepository heroRepository = new HeroRepository();
        Assert.Throws<ArgumentNullException>(() => heroRepository.Create(hero));
    }

    [Test]
    public void WhenHeroRepositoryCreatesHero_HeroAlreadyExistingShouldThrowException()
    {
        Hero hero = new Hero("Pesho", 8);
        Hero hero2 = new Hero("Pesho", 10);
        HeroRepository heroRepository = new HeroRepository();
        heroRepository.Create(hero);
        Assert.Throws<InvalidOperationException>(() => heroRepository.Create(hero2));
    }

    [Test]
    public void WhenHeroRepositoryCreatesHero_HeroesCountShouldIncrease()
    {
        Hero hero = new Hero("Pesho", 8);
        HeroRepository heroRepository = new HeroRepository();
        heroRepository.Create(hero);
        Assert.AreEqual(heroRepository.Heroes.Count, 1);
    }

    [Test]
    public void WhenHeroRepositoryRemovesHero_HeroNameNullOrWhitespace_ShouldThrowException()
    {
        Hero hero = new Hero("Pesho", 8);
        HeroRepository heroRepository = new HeroRepository();
        heroRepository.Create(hero);
        Assert.Throws<ArgumentNullException>(() => heroRepository.Remove(null));
        Assert.Throws<ArgumentNullException>(() => heroRepository.Remove(" "));
    }

    [Test]
    public void WhenHeroRepositoryRemovesHero_HeroesCountShouldBeReduced()
    {
        Hero hero = new Hero("Pesho", 8);
        HeroRepository heroRepository = new HeroRepository();
        heroRepository.Create(hero);
        heroRepository.Remove("Pesho");
        Assert.AreEqual(heroRepository.Heroes.Count, 0);
    }

    [Test]
    public void WhenGettingHeroWithHighestLevel_ShouldReturnCorrectlyTheHighestLevelHero()
    {
        Hero hero = new Hero("Pesho", 8);
        Hero hero2 = new Hero("Ivan", 15);
        HeroRepository heroRepository = new HeroRepository();
        heroRepository.Create(hero);
        heroRepository.Create(hero2);
        Assert.AreEqual(heroRepository.GetHeroWithHighestLevel(), hero2);
    }

    [Test]

    public void WhenGettingHeroByName_ShouldReturnCorrectHero()
    {
        Hero hero = new Hero("Pesho", 8);
        Hero hero2 = new Hero("Ivan", 15);
        HeroRepository heroRepository = new HeroRepository();
        heroRepository.Create(hero);
        heroRepository.Create(hero2);
        Assert.AreEqual(heroRepository.GetHero("Pesho"), hero);
    }
}