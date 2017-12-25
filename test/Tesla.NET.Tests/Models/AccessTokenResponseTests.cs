﻿// Copyright (c) 2018 James Skimming. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

namespace Tesla.NET.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoFixture;
    using FluentAssertions;
    using Newtonsoft.Json.Linq;
    using Xunit;
    using Xunit.Abstractions;

    public class When_serializing_AccessTokenResponse_Should_serialize
    {
        private readonly AccessTokenResponse _sut;
        private readonly JObject _json;

        public When_serializing_AccessTokenResponse_Should_serialize(ITestOutputHelper output)
        {
            IFixture fixture = new Fixture().Customize(new TeslaNetCustomization());
            _sut = fixture.Create<AccessTokenResponse>();
            _json = JObject.FromObject(_sut);

            output.WriteLine("Serialized JSON:" + Environment.NewLine + _json);
        }

        [Fact]
        public void five_properties() => _json.Count.Should().Be(5);

        [Fact]
        public void access_token() => _json["access_token"].Value<string>().Should().Be(_sut.AccessToken);

        [Fact]
        public void token_type() => _json["token_type"].Value<string>().Should().Be(_sut.TokenType);

        [Fact]
        public void expires_in() => _json["expires_in"].Value<long>().Should().Be(_sut.ExpiresIn);

        [Fact]
        public void refresh_token() => _json["refresh_token"].Value<string>().Should().Be(_sut.RefreshToken);

        [Fact]
        public void created_at() => _json["created_at"].Value<long>().Should().Be(_sut.CreatedAt);
    }

    public class When_serializing_and_deserializing_AccessTokenResponse
    {
        private readonly AccessTokenResponse _expected;
        private readonly AccessTokenResponse _actual;

        public When_serializing_and_deserializing_AccessTokenResponse(ITestOutputHelper output)
        {
            IFixture fixture = new Fixture().Customize(new TeslaNetCustomization());
            _expected = fixture.Create<AccessTokenResponse>();
            JObject json = JObject.FromObject(_expected);

            output.WriteLine("Serialized JSON:" + Environment.NewLine + json);

            _actual = json.ToObject<AccessTokenResponse>();
        }

        [Fact]
        public void Should_retain_all_properties() => _actual.AsLikeness().ShouldEqual(_expected);
    }

    public class When_deserializing_AccessTokenResponse_Should_deserialize
    {
        private readonly AccessTokenResponse _sut;
        private readonly JObject _json;

        public When_deserializing_AccessTokenResponse_Should_deserialize(ITestOutputHelper output)
        {
            _json = SampleJson.AccessTokenResponse;
            _sut = _json.ToObject<AccessTokenResponse>();

            output.WriteLine("Serialized JSON:" + Environment.NewLine + _json);
        }

        [Fact]
        public void access_token() => _sut.AccessToken.Should().Be(_json["access_token"].Value<string>());

        [Fact]
        public void token_type() => _sut.TokenType.Should().Be(_json["token_type"].Value<string>());

        [Fact]
        public void expires_in() => _sut.ExpiresIn.Should().Be(_json["expires_in"].Value<long>());

        [Fact]
        public void refresh_token() => _sut.RefreshToken.Should().Be(_json["refresh_token"].Value<string>());

        [Fact]
        public void created_at() => _sut.CreatedAt.Should().Be(_json["created_at"].Value<long>());
    }
}