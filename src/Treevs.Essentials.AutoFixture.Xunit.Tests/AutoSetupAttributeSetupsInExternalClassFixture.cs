﻿using System;

namespace Treevs.Essentials.AutoFixture.Xunit.Tests
{
    using global::Xunit;
    using global::Xunit.Extensions;

    using Ploeh.AutoFixture;

    using Treevs.Essentials.AutoFixture.Xunit.AutoSetup;

    public class MyAutoSetups
    {
        public static Action<IFixture> AutoSetup()
        {
            return (f) =>
            {
                f.Customize<AutoSetupGlobalSut>(obj => obj.OmitAutoProperties());
                f.Customize<AutoSetupMethodSut>(obj => obj.OmitAutoProperties());
                f.Customize<AutoSetupPropertySut>(obj => obj.OmitAutoProperties());

                f.Customize<AutoSetupGlobalSut>(obj => obj.Do(sut => sut.Setup = true));
            };
        }

        public static Action<IFixture> MethodSetup()
        {
            return (f) => f.Customize<AutoSetupMethodSut>(obj => obj
                .Do(sut => sut.Setup = true)
                .OmitAutoProperties());
        }

        public static Action<IFixture> PropertySetup
        {
            get
            {
                return (f) => f.Customize<AutoSetupPropertySut>(obj => obj
                    .Do(sut => sut.Setup = true)
                    .OmitAutoProperties());
            }
        }
    }

    public class AutoSetupAttributeSetupsInExternalClassFixture
    {
        [Theory]
        [AutoSetup(typeof(MyAutoSetups))]
        public void ImplicitGlobalSetup(
            AutoSetupGlobalSut globalSut, 
            AutoSetupMethodSut methodSut, 
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.False(methodSut.Setup);
            Assert.False(propertySut.Setup);
        }

        [Theory]
        [AutoSetup(typeof(MyAutoSetups), "AutoSetup")]
        public void ExplicitGlobalSetup(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.False(methodSut.Setup);
            Assert.False(propertySut.Setup);
        }

        [Theory]
        [AutoSetup(typeof(MyAutoSetups), "AutoSetup", "MethodSetup")]
        public void ExplicitGlobalSetupWithMethodSetup(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.True(methodSut.Setup);
            Assert.False(propertySut.Setup);
        }

        [Theory]
        [AutoSetup(typeof(MyAutoSetups), "MethodSetup")]
        public void GlobalSetupWithMethodSetup(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.True(methodSut.Setup);
            Assert.False(propertySut.Setup);
        }

        [Theory]
        [AutoSetup(typeof(MyAutoSetups), "PropertySetup")]
        public void GlobalSetupWithPropertySetup(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.False(methodSut.Setup);
            Assert.True(propertySut.Setup);
        }

        [Theory]
        [AutoSetup(typeof(MyAutoSetups), "MethodSetup", "PropertySetup")]
        public void GlobalSetupWithMultipleSetups(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.True(methodSut.Setup);
            Assert.True(propertySut.Setup);
        }

        [Theory]
        [AutoSetup(typeof(MyAutoSetups), "MethodSetup", "PropertySetup")]
        public void FixtureInstanceInjected(IFixture fixture)
        {
            Assert.True(fixture.Create<AutoSetupGlobalSut>().Setup);
            Assert.True(fixture.Create<AutoSetupMethodSut>().Setup);
            Assert.True(fixture.Create<AutoSetupPropertySut>().Setup);
        }
    }

    public class AutoSetupAttributeSetupsInExternalClassViaSourcePropertyFixture
    {
        // specifies this type will be used as the source of all setup methods, unless explicitly
        // overriden for a test method.
        public static Type AutoSetupSource = typeof(MyAutoSetups);

        [Theory]
        [AutoSetup]
        public void ImplicitGlobalSetup(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.False(methodSut.Setup);
            Assert.False(propertySut.Setup);
        }

        [Theory]
        [AutoSetup("AutoSetup")]
        public void ExplicitGlobalSetup(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.False(methodSut.Setup);
            Assert.False(propertySut.Setup);
        }

        [Theory]
        [AutoSetup("AutoSetup", "MethodSetup")]
        public void ExplicitGlobalSetupWithMethodSetup(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.True(methodSut.Setup);
            Assert.False(propertySut.Setup);
        }

        [Theory]
        [AutoSetup("MethodSetup")]
        public void GlobalSetupWithMethodSetup(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.True(methodSut.Setup);
            Assert.False(propertySut.Setup);
        }

        [Theory]
        [AutoSetup("PropertySetup")]
        public void GlobalSetupWithPropertySetup(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.False(methodSut.Setup);
            Assert.True(propertySut.Setup);
        }

        [Theory]
        [AutoSetup("MethodSetup", "PropertySetup")]
        public void GlobalSetupWithMultipleSetups(
            AutoSetupGlobalSut globalSut,
            AutoSetupMethodSut methodSut,
            AutoSetupPropertySut propertySut)
        {
            Assert.True(globalSut.Setup);
            Assert.True(methodSut.Setup);
            Assert.True(propertySut.Setup);
        }

        [Theory]
        [AutoSetup("MethodSetup", "PropertySetup")]
        public void FixtureInstanceInjected(IFixture fixture)
        {
            Assert.True(fixture.Create<AutoSetupGlobalSut>().Setup);
            Assert.True(fixture.Create<AutoSetupMethodSut>().Setup);
            Assert.True(fixture.Create<AutoSetupPropertySut>().Setup);
        }
    }
}
