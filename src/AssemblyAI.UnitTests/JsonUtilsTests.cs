using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using AssemblyAI.Core;
using AssemblyAI.Transcripts;
using NUnit.Framework;

namespace AssemblyAI.UnitTests;

[TestFixture]
public class JsonUtilsTests
{
    #region Consts

    private const string TranscriptId = "9ea68fd3-f953-42c1-9742-976c447fb463";

    [StringSyntax(StringSyntaxAttribute.Json)]
    private const string TranscriptJson = $$"""
                                            {
                                              "id": "{{TranscriptId}}",
                                              "speech_model": null,
                                              "language_model": "assemblyai_default",
                                              "acoustic_model": "assemblyai_default",
                                              "language_code": "en_us",
                                              "language_detection": true,
                                              "language_confidence_threshold": 0.7,
                                              "language_confidence": 0.9959,
                                              "status": "completed",
                                              "audio_url": "https://assembly.ai/wildfires.mp3",
                                              "text": "Smoke from hundreds of wildfires in Canada is triggering air quality alerts throughout the US. Skylines from Maine to Maryland to Minnesota are gray and smoggy. And in some places, the air quality warnings include the warning to stay inside. We wanted to better understand what's happening here and why, so we called Peter de Carlo, an associate professor in the Department of Environmental Health and Engineering at Johns Hopkins University Varsity. Good morning, professor. Good morning. What is it about the conditions right now that have caused this round of wildfires to affect so many people so far away? Well, there's a couple of things. The season has been pretty dry already. And then the fact that we're getting hit in the US. Is because there's a couple of weather systems that are essentially channeling the smoke from those Canadian wildfires through Pennsylvania into the Mid Atlantic and the Northeast and kind of just dropping the smoke there. So what is it in this haze that makes it harmful? And I'm assuming it is harmful. It is. The levels outside right now in Baltimore are considered unhealthy. And most of that is due to what's called particulate matter, which are tiny particles, microscopic smaller than the width of your hair that can get into your lungs and impact your respiratory system, your cardiovascular system, and even your neurological your brain. What makes this particularly harmful? Is it the volume of particulant? Is it something in particular? What is it exactly? Can you just drill down on that a little bit more? Yeah. So the concentration of particulate matter I was looking at some of the monitors that we have was reaching levels of what are, in science, big 150 micrograms per meter cubed, which is more than ten times what the annual average should be and about four times higher than what you're supposed to have on a 24 hours average. And so the concentrations of these particles in the air are just much, much higher than we typically see. And exposure to those high levels can lead to a host of health problems. And who is most vulnerable? I noticed that in New York City, for example, they're canceling outdoor activities. And so here it is in the early days of summer, and they have to keep all the kids inside. So who tends to be vulnerable in a situation like this? It's the youngest. So children, obviously, whose bodies are still developing. The elderly, who are their bodies are more in decline and they're more susceptible to the health impacts of breathing, the poor air quality. And then people who have preexisting health conditions, people with respiratory conditions or heart conditions can be triggered by high levels of air pollution. Could this get worse? That's a good question. In some areas, it's much worse than others. And it just depends on kind of where the smoke is concentrated. I think New York has some of the higher concentrations right now, but that's going to change as that air moves away from the New York area. But over the course of the next few days, we will see different areas being hit at different times with the highest concentrations. I was going to ask you about more fires start burning. I don't expect the concentrations to go up too much higher. I was going to ask you how and you started to answer this, but how much longer could this last? Or forgive me if I'm asking you to speculate, but what do you think? Well, I think the fires are going to burn for a little bit longer, but the key for us in the US. Is the weather system changing. And so right now, it's kind of the weather systems that are pulling that air into our mid Atlantic and Northeast region. As those weather systems change and shift, we'll see that smoke going elsewhere and not impact us in this region as much. And so I think that's going to be the defining factor. And I think the next couple of days we're going to see a shift in that weather pattern and start to push the smoke away from where we are. And finally, with the impacts of climate change, we are seeing more wildfires. Will we be seeing more of these kinds of wide ranging air quality consequences or circumstances? I mean, that is one of the predictions for climate change. Looking into the future, the fire season is starting earlier and lasting longer, and we're seeing more frequent fires. So, yeah, this is probably something that we'll be seeing more frequently. This tends to be much more of an issue in the Western US. So the eastern US. Getting hit right now is a little bit new. But yeah, I think with climate change moving forward, this is something that is going to happen more frequently. That's Peter De Carlo, associate professor in the Department of Environmental Health and Engineering at Johns Hopkins University. Sergeant Carlo, thanks so much for joining us and sharing this expertise with us. Thank you for having me.",
                                              "words": [
                                                {
                                                  "text": "Smoke",
                                                  "start": 250,
                                                  "end": 650,
                                                  "confidence": 0.97465,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "from",
                                                  "start": 730,
                                                  "end": 1022,
                                                  "confidence": 0.99999,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "hundreds",
                                                  "start": 1076,
                                                  "end": 1418,
                                                  "confidence": 0.99844,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "of",
                                                  "start": 1434,
                                                  "end": 1614,
                                                  "confidence": 0.84,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "wildfires",
                                                  "start": 1652,
                                                  "end": 2346,
                                                  "confidence": 0.89572,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "in",
                                                  "start": 2378,
                                                  "end": 2526,
                                                  "confidence": 0.99994,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "Canada",
                                                  "start": 2548,
                                                  "end": 3130,
                                                  "confidence": 0.93953,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "is",
                                                  "start": 3210,
                                                  "end": 3454,
                                                  "confidence": 0.999,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "triggering",
                                                  "start": 3492,
                                                  "end": 3946,
                                                  "confidence": 0.74794,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "air",
                                                  "start": 3978,
                                                  "end": 4174,
                                                  "confidence": 1,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "quality",
                                                  "start": 4212,
                                                  "end": 4558,
                                                  "confidence": 0.88077,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "alerts",
                                                  "start": 4644,
                                                  "end": 5114,
                                                  "confidence": 0.94814,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "throughout",
                                                  "start": 5162,
                                                  "end": 5466,
                                                  "confidence": 0.99726,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "the",
                                                  "start": 5498,
                                                  "end": 5694,
                                                  "confidence": 0.79,
                                                  "speaker": null
                                                },
                                                {
                                                  "text": "US.",
                                                  "start": 5732,
                                                  "end": 6382,
                                                  "confidence": 0.89,
                                                  "speaker": null
                                                }
                                              ],
                                              "utterances": [
                                                {
                                                  "confidence": 0.9359033333333334,
                                                  "end": 26950,
                                                  "speaker": "A",
                                                  "start": 250,
                                                  "text": "Smoke from hundreds of wildfires in Canada is triggering air quality alerts throughout the US. Skylines from Maine to Maryland to Minnesota are gray and smoggy. And in some places, the air quality warnings include the warning to stay inside. We wanted to better understand what's happening here and why, so we called Peter de Carlo, an associate professor in the Department of Environmental Health and Engineering at Johns Hopkins University Varsity. Good morning, professor.",
                                                  "words": [
                                                    {
                                                      "text": "Smoke",
                                                      "start": 250,
                                                      "end": 650,
                                                      "confidence": 0.97503,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "from",
                                                      "start": 730,
                                                      "end": 1022,
                                                      "confidence": 0.99999,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "hundreds",
                                                      "start": 1076,
                                                      "end": 1418,
                                                      "confidence": 0.99843,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "of",
                                                      "start": 1434,
                                                      "end": 1614,
                                                      "confidence": 0.85,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "wildfires",
                                                      "start": 1652,
                                                      "end": 2346,
                                                      "confidence": 0.89657,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "in",
                                                      "start": 2378,
                                                      "end": 2526,
                                                      "confidence": 0.99994,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "Canada",
                                                      "start": 2548,
                                                      "end": 3130,
                                                      "confidence": 0.93864,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "is",
                                                      "start": 3210,
                                                      "end": 3454,
                                                      "confidence": 0.999,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "triggering",
                                                      "start": 3492,
                                                      "end": 3946,
                                                      "confidence": 0.75366,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "air",
                                                      "start": 3978,
                                                      "end": 4174,
                                                      "confidence": 1,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "quality",
                                                      "start": 4212,
                                                      "end": 4558,
                                                      "confidence": 0.87745,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "alerts",
                                                      "start": 4644,
                                                      "end": 5114,
                                                      "confidence": 0.94739,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "throughout",
                                                      "start": 5162,
                                                      "end": 5466,
                                                      "confidence": 0.99726,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "the",
                                                      "start": 5498,
                                                      "end": 5694,
                                                      "confidence": 0.79,
                                                      "speaker": "A"
                                                    },
                                                    {
                                                      "text": "US.",
                                                      "start": 5732,
                                                      "end": 6382,
                                                      "confidence": 0.88,
                                                      "speaker": "A"
                                                    }
                                                  ]
                                                }
                                              ],
                                              "confidence": 0.9404651451800253,
                                              "audio_duration": 281,
                                              "punctuate": true,
                                              "format_text": true,
                                              "dual_channel": false,
                                              "webhook_url": "https://your-webhook-url.tld/path",
                                              "webhook_status_code": 200,
                                              "webhook_auth": true,
                                              "webhook_auth_header_name": "webhook-secret",
                                              "auto_highlights_result": {
                                                "status": "success",
                                                "results": [
                                                  {
                                                    "count": 1,
                                                    "rank": 0.08,
                                                    "text": "air quality alerts",
                                                    "timestamps": [
                                                      {
                                                        "start": 3978,
                                                        "end": 5114
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 1,
                                                    "rank": 0.08,
                                                    "text": "wide ranging air quality consequences",
                                                    "timestamps": [
                                                      {
                                                        "start": 235388,
                                                        "end": 238694
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 1,
                                                    "rank": 0.07,
                                                    "text": "more wildfires",
                                                    "timestamps": [
                                                      {
                                                        "start": 230972,
                                                        "end": 232354
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 1,
                                                    "rank": 0.07,
                                                    "text": "air pollution",
                                                    "timestamps": [
                                                      {
                                                        "start": 156004,
                                                        "end": 156910
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 3,
                                                    "rank": 0.07,
                                                    "text": "weather systems",
                                                    "timestamps": [
                                                      {
                                                        "start": 47344,
                                                        "end": 47958
                                                      },
                                                      {
                                                        "start": 205268,
                                                        "end": 205818
                                                      },
                                                      {
                                                        "start": 211588,
                                                        "end": 213434
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 2,
                                                    "rank": 0.06,
                                                    "text": "high levels",
                                                    "timestamps": [
                                                      {
                                                        "start": 121128,
                                                        "end": 121646
                                                      },
                                                      {
                                                        "start": 155412,
                                                        "end": 155866
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 1,
                                                    "rank": 0.06,
                                                    "text": "health conditions",
                                                    "timestamps": [
                                                      {
                                                        "start": 152138,
                                                        "end": 152666
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 2,
                                                    "rank": 0.06,
                                                    "text": "Peter de Carlo",
                                                    "timestamps": [
                                                      {
                                                        "start": 18948,
                                                        "end": 19930
                                                      },
                                                      {
                                                        "start": 268298,
                                                        "end": 269194
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 1,
                                                    "rank": 0.06,
                                                    "text": "New York City",
                                                    "timestamps": [
                                                      {
                                                        "start": 125768,
                                                        "end": 126274
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 1,
                                                    "rank": 0.05,
                                                    "text": "respiratory conditions",
                                                    "timestamps": [
                                                      {
                                                        "start": 152964,
                                                        "end": 153786
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 3,
                                                    "rank": 0.05,
                                                    "text": "New York",
                                                    "timestamps": [
                                                      {
                                                        "start": 125768,
                                                        "end": 126034
                                                      },
                                                      {
                                                        "start": 171448,
                                                        "end": 171938
                                                      },
                                                      {
                                                        "start": 176008,
                                                        "end": 176322
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 3,
                                                    "rank": 0.05,
                                                    "text": "climate change",
                                                    "timestamps": [
                                                      {
                                                        "start": 229548,
                                                        "end": 230230
                                                      },
                                                      {
                                                        "start": 244576,
                                                        "end": 245162
                                                      },
                                                      {
                                                        "start": 263348,
                                                        "end": 263950
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 1,
                                                    "rank": 0.05,
                                                    "text": "Johns Hopkins University Varsity",
                                                    "timestamps": [
                                                      {
                                                        "start": 23972,
                                                        "end": 25490
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 1,
                                                    "rank": 0.05,
                                                    "text": "heart conditions",
                                                    "timestamps": [
                                                      {
                                                        "start": 153988,
                                                        "end": 154506
                                                      }
                                                    ]
                                                  },
                                                  {
                                                    "count": 1,
                                                    "rank": 0.05,
                                                    "text": "air quality warnings",
                                                    "timestamps": [
                                                      {
                                                        "start": 12308,
                                                        "end": 13434
                                                      }
                                                    ]
                                                  }
                                                ]
                                              },
                                              "auto_highlights": true,
                                              "audio_start_from": 10,
                                              "audio_end_at": 280,
                                              "word_boost": [
                                                "aws",
                                                "azure",
                                                "google cloud"
                                              ],
                                              "boost_param": "high",
                                              "filter_profanity": true,
                                              "redact_pii": true,
                                              "redact_pii_audio": true,
                                              "redact_pii_audio_quality": "mp3",
                                              "redact_pii_policies": [
                                                "us_social_security_number",
                                                "credit_card_number"
                                              ],
                                              "redact_pii_sub": "hash",
                                              "speaker_labels": true,
                                              "content_safety": true,
                                              "iab_categories": true,
                                              "content_safety_labels": {
                                                "status": "success",
                                                "results": [
                                                  {
                                                    "text": "Smoke from hundreds of wildfires in Canada is triggering air quality alerts throughout the US. Skylines from Maine to Maryland to Minnesota are gray and smoggy. And in some places, the air quality warnings include the warning to stay inside. We wanted to better understand what's happening here and why, so we called Peter de Carlo, an associate professor in the Department of Environmental Health and Engineering at Johns Hopkins University Varsity. Good morning, professor. Good morning.",
                                                    "labels": [
                                                      {
                                                        "label": "disasters",
                                                        "confidence": 0.8142836093902588,
                                                        "severity": 0.4093044400215149
                                                      }
                                                    ],
                                                    "sentences_idx_start": 0,
                                                    "sentences_idx_end": 5,
                                                    "timestamp": {
                                                      "start": 250,
                                                      "end": 28840
                                                    }
                                                  }
                                                ],
                                                "summary": {
                                                  "disasters": 0.9940800441842205,
                                                  "health_issues": 0.9216489289040967
                                                },
                                                "severity_score_summary": {
                                                  "disasters": {
                                                    "low": 0.5733263024656846,
                                                    "medium": 0.42667369753431533,
                                                    "high": 0
                                                  },
                                                  "health_issues": {
                                                    "low": 0.22863814977924785,
                                                    "medium": 0.45014154926938227,
                                                    "high": 0.32122030095136983
                                                  }
                                                }
                                              },
                                              "iab_categories_result": {
                                                "status": "success",
                                                "results": [
                                                  {
                                                    "text": "Smoke from hundreds of wildfires in Canada is triggering air quality alerts throughout the US. Skylines from Maine to Maryland to Minnesota are gray and smoggy. And in some places, the air quality warnings include the warning to stay inside. We wanted to better understand what's happening here and why, so we called Peter de Carlo, an associate professor in the Department of Environmental Health and Engineering at Johns Hopkins University Varsity. Good morning, professor. Good morning.",
                                                    "labels": [
                                                      {
                                                        "relevance": 0.988274097442627,
                                                        "label": "Home&Garden>IndoorEnvironmentalQuality"
                                                      },
                                                      {
                                                        "relevance": 0.5821335911750793,
                                                        "label": "NewsAndPolitics>Weather"
                                                      },
                                                      {
                                                        "relevance": 0.0042327106930315495,
                                                        "label": "MedicalHealth>DiseasesAndConditions>LungAndRespiratoryHealth"
                                                      },
                                                      {
                                                        "relevance": 0.0033971222583204508,
                                                        "label": "NewsAndPolitics>Disasters"
                                                      },
                                                      {
                                                        "relevance": 0.002469958271831274,
                                                        "label": "BusinessAndFinance>Business>GreenSolutions"
                                                      },
                                                      {
                                                        "relevance": 0.0014376690378412604,
                                                        "label": "MedicalHealth>DiseasesAndConditions>Cancer"
                                                      },
                                                      {
                                                        "relevance": 0.0014294233405962586,
                                                        "label": "Science>Environment"
                                                      },
                                                      {
                                                        "relevance": 0.001234519761055708,
                                                        "label": "Travel>TravelLocations>PolarTravel"
                                                      },
                                                      {
                                                        "relevance": 0.0010231725173071027,
                                                        "label": "MedicalHealth>DiseasesAndConditions>ColdAndFlu"
                                                      },
                                                      {
                                                        "relevance": 0.0007445293595083058,
                                                        "label": "BusinessAndFinance>Industries>PowerAndEnergyIndustry"
                                                      }
                                                    ],
                                                    "timestamp": {
                                                      "start": 250,
                                                      "end": 28840
                                                    }
                                                  }
                                                ],
                                                "summary": {
                                                  "NewsAndPolitics>Weather": 1,
                                                  "Home&Garden>IndoorEnvironmentalQuality": 0.9043831825256348,
                                                  "Science>Environment": 0.16117265820503235,
                                                  "BusinessAndFinance>Industries>EnvironmentalServicesIndustry": 0.14393523335456848,
                                                  "MedicalHealth>DiseasesAndConditions>LungAndRespiratoryHealth": 0.11401086300611496,
                                                  "BusinessAndFinance>Business>GreenSolutions": 0.06348437070846558,
                                                  "NewsAndPolitics>Disasters": 0.05041387677192688,
                                                  "Travel>TravelLocations>PolarTravel": 0.01308488193899393,
                                                  "HealthyLiving": 0.008222488686442375,
                                                  "MedicalHealth>DiseasesAndConditions>ColdAndFlu": 0.0022315620444715023,
                                                  "MedicalHealth>DiseasesAndConditions>HeartAndCardiovascularDiseases": 0.00213034451007843,
                                                  "HealthyLiving>Wellness>SmokingCessation": 0.001540527562610805,
                                                  "MedicalHealth>DiseasesAndConditions>Injuries": 0.0013950627762824297,
                                                  "BusinessAndFinance>Industries>PowerAndEnergyIndustry": 0.0012570273829624057,
                                                  "MedicalHealth>DiseasesAndConditions>Cancer": 0.001097781932912767,
                                                  "MedicalHealth>DiseasesAndConditions>Allergies": 0.0010148967849090695,
                                                  "MedicalHealth>DiseasesAndConditions>MentalHealth": 0.000717321818228811,
                                                  "Style&Fashion>PersonalCare>DeodorantAndAntiperspirant": 0.0006022014422342181,
                                                  "Technology&Computing>Computing>ComputerNetworking": 0.0005461975233629346,
                                                  "MedicalHealth>DiseasesAndConditions>Injuries>FirstAid": 0.0004885646631009877
                                                }
                                              },
                                              "custom_spelling": null,
                                              "throttled": null,
                                              "auto_chapters": true,
                                              "summarization": true,
                                              "summary_type": "bullets",
                                              "summary_model": "informative",
                                              "custom_topics": true,
                                              "topics": [],
                                              "speech_threshold": 0.5,
                                              "disfluencies": false,
                                              "sentiment_analysis": true,
                                              "chapters": [
                                                {
                                                  "summary": "Smoke from hundreds of wildfires in Canada is triggering air quality alerts throughout the US. Skylines from Maine to Maryland to Minnesota are gray and smoggy. In some places, the air quality warnings include the warning to stay inside.",
                                                  "gist": "Smoggy air quality alerts across US",
                                                  "headline": "Smoke from hundreds of wildfires in Canada is triggering air quality alerts across US",
                                                  "start": 250,
                                                  "end": 28840
                                                },
                                                {
                                                  "summary": "Air pollution levels in Baltimore are considered unhealthy. Exposure to high levels can lead to a host of health problems. With climate change, we are seeing more wildfires. Will we be seeing more of these kinds of wide ranging air quality consequences?",
                                                  "gist": "What is it about the conditions right now that have caused this round",
                                                  "headline": "High particulate matter in wildfire smoke can lead to serious health problems",
                                                  "start": 29610,
                                                  "end": 280340
                                                }
                                              ],
                                              "sentiment_analysis_results": null,
                                              "entity_detection": true,
                                              "entities": [
                                                {
                                                  "entity_type": "location",
                                                  "text": "Canada",
                                                  "start": 2548,
                                                  "end": 3130
                                                },
                                                {
                                                  "entity_type": "location",
                                                  "text": "the US",
                                                  "start": 5498,
                                                  "end": 6382
                                                },
                                                {
                                                  "entity_type": "location",
                                                  "text": "Maine",
                                                  "start": 7492,
                                                  "end": 7914
                                                },
                                                {
                                                  "entity_type": "location",
                                                  "text": "Maryland",
                                                  "start": 8212,
                                                  "end": 8634
                                                },
                                                {
                                                  "entity_type": "location",
                                                  "text": "Minnesota",
                                                  "start": 8932,
                                                  "end": 9578
                                                },
                                                {
                                                  "entity_type": "person_name",
                                                  "text": "Peter de Carlo",
                                                  "start": 18948,
                                                  "end": 19930
                                                },
                                                {
                                                  "entity_type": "occupation",
                                                  "text": "associate professor",
                                                  "start": 20292,
                                                  "end": 21194
                                                },
                                                {
                                                  "entity_type": "organization",
                                                  "text": "Department of Environmental Health and Engineering",
                                                  "start": 21508,
                                                  "end": 23706
                                                },
                                                {
                                                  "entity_type": "organization",
                                                  "text": "Johns Hopkins University Varsity",
                                                  "start": 23972,
                                                  "end": 25490
                                                },
                                                {
                                                  "entity_type": "occupation",
                                                  "text": "professor",
                                                  "start": 26076,
                                                  "end": 26950
                                                },
                                                {
                                                  "entity_type": "location",
                                                  "text": "the US",
                                                  "start": 45184,
                                                  "end": 45898
                                                },
                                                {
                                                  "entity_type": "nationality",
                                                  "text": "Canadian",
                                                  "start": 49728,
                                                  "end": 50086
                                                }
                                              ],
                                              "summary": "- Smoke from hundreds of wildfires in Canada is triggering air quality alerts throughout the US. Skylines from Maine to Maryland to Minnesota are gray and smoggy. In some places, the air quality warnings include the warning to stay inside.- Air pollution levels in Baltimore are considered unhealthy. Exposure to high levels can lead to a host of health problems. With climate change, we are seeing more wildfires. Will we be seeing more of these kinds of wide ranging air quality consequences?",
                                              "speakers_expected": 2
                                            }
                                            """;

    #endregion

    [Test]
    public void FromJsonString()
    {
      var transcript = JsonUtils.Deserialize<Transcript>(TranscriptJson);
        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
        });
    }

    [Test]
    public void FromJsonElement()
    {
        var transcript = JsonUtils.Deserialize<Transcript>(TranscriptJson);
        var json = JsonUtils.SerializeToElement(transcript);
        transcript = JsonUtils.Deserialize<Transcript>(json);
        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.Not.Null);
            Assert.That(transcript.Text, Is.Not.Empty);
        });
    }

    [Test]
    public void FromJsonDocument()
    {
        var transcript = JsonUtils.Deserialize<Transcript>(TranscriptJson);
        var json = JsonUtils.SerializeToDocument(transcript);
        transcript = JsonUtils.Deserialize<Transcript>(json);
        Assert.That(transcript, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.EqualTo(TranscriptId));
            Assert.That(transcript.Text, Is.Not.Empty);
        });
    }

    [Test]
    public void FromJsonNode()
    {
        var transcript = JsonUtils.Deserialize<Transcript>(TranscriptJson);
        var json = JsonUtils.SerializeToNode(transcript)!;
        transcript = JsonUtils.Deserialize<Transcript>(json);
        Assert.Multiple(() =>
        {
            Assert.That(transcript.Id, Is.EqualTo(TranscriptId));
            Assert.That(transcript.Text, Is.Not.Empty);
        });
    }

    [Test]
    public void ToJsonString()
    {
        var transcript = JsonUtils.Deserialize<Transcript>(TranscriptJson);
        var json = JsonUtils.Serialize(transcript);
        Assert.Multiple(() =>
        {
            Assert.That(json, Is.Not.Empty);
            Assert.That(json, Contains.Substring("\"9ea68fd3-f953-42c1-9742-976c447fb463\""));
        });
    }

    [Test]
    public void ToJsonElement()
    {
        var transcript = JsonUtils.Deserialize<Transcript>(TranscriptJson);
        var json = JsonUtils.SerializeToElement(transcript);
        Assert.That(json.GetProperty("id").GetString(), Is.EqualTo(TranscriptId));
    }

    [Test]
    public void ToJsonDocument()
    {
        var transcript = JsonUtils.Deserialize<Transcript>(TranscriptJson);
        var json = JsonUtils.SerializeToDocument(transcript);
        Assert.That(json.RootElement.GetProperty("id").GetString(), Is.EqualTo(TranscriptId));
    }

    [Test]
    public void ToJsonNode()
    {
        var transcript = JsonUtils.Deserialize<Transcript>(TranscriptJson);
        var json = JsonUtils.SerializeToNode(transcript);
        Assert.That(json, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(json, Is.Not.Empty);
            Assert.That(json.AsObject()["id"]!.GetValue<string>(), Is.EqualTo(TranscriptId));
        });
    }
}