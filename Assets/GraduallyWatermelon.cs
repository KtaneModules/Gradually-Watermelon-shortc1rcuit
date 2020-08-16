/*MESSAGE TO ANY FUTURE CODERS:
 PLEASE COMMENT YOUR WORK
 I can't stress how important this is especially with bomb types such as boss modules.
 If you don't it makes it realy hard for somone like me to find out how a module is working so I can learn how to make my own.
 Please comment your work.
 Short_c1rcuit*/

using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KModkit;

public class GraduallyWatermelon : MonoBehaviour
{
	//Gets audio clips and info about the bomb.
	public KMAudio audio;
	public KMBombInfo bomb;

	//Both buttons on the bomb
	public KMSelectable left;
	public KMSelectable right;

	//Submit button
	public KMSelectable submit;

	//Text on the module
	public Text lyric;
	public Text song;

	//Indexes of the chosen song and lyric in the arrays
	int selectedSong;
	int selectedLyric;

	//The index of the song on the bottom screen
	int currentSong;

	//The current stage you're on (0-indexed)
	int stage;

	//The names for the songs
	string[] songNames = new string[25] { "Africa", "All I Want For Christmas Is You", "All Star", "Bohemian Rhapsody", "Country Roads", "Despacito", "Havana", "I Don't Care", "Into the Unknown", "Look What You Made Me Do", "Memory", "Mother Knows Best", "New Rules", "No Tears Left to Cry", "Old Town Road", "Señorita", "Shape of You", "Sucker", "Take Me to Church", "The Middle", "The Sound of Silence", "This is Halloween", "Thriller", "Truth Hurts", "You're Welcome" };

	//All of the lyrics used in the module
	string[][,] lyrics = {
							new string[,]{ { "Anime, cristemopois diaosomia dibesi compressed diabetic watermelon", "africa0" }, { "Problem with counting, 2430 feathers", "africa1" }, { "On the road I was defeated by an old man", "africa2" }, { "I will take your lunch away from you", "africa3" }, { "You can not seduce hundreds of people", "africa4" }, { "I'm having ice cream", "africa5" }, { "Time to get drunk", "africa6" }, { "Don't eat Dogs", "africa7" }, { "I try to feel deep about myself", "africa8" }, { "I did nothing", "africa9" } },
							new string[,]{ { "I am a duck in the shade of a tree", "alliwant0" }, { "Birthday other Diploflix sports", "alliwant1" }, { "Birthday other Diploflix sports", "alliwant1" }, { "I want to be alone", "alliwant2" }, { "Click here for more information", "alliwant3" }, { "I want you here today as the Prime Minister's problems", "alliwant4" }, { "What are you doing?", "alliwant5" }, { "Ooh, boys", "alliwant6" }, { "Listen to the sledge swimming pool ringtone", "alliwant7" }, { "Will you have my kids with me?", "alliwant8" }, { "Oh I just love me", "alliwant9" } },
							new string[,]{ { "He saw his fingers, fingers and fingers", "allstar0" }, { "Yes, the revolution's going on, I don't believe the truth, you go", "allstar1" }, { "Surprisingly, life does not make sense", "allstar2" }, { "My daughter has no knowledge of branches", "allstar3" }, { "Hi, you are not very funny", "allstar4" }, { "Poland", "allstar5" }, { "Only by washing a picture of a mushroom", "allstar6" }, { "It's lucky, but to become Dodo Conservative", "allstar7" }, { "How can you wash hot water", "allstar8" }, { "Not now, a college lawyer is looking to kill me", "allstar9" } },
							new string[,]{ { "What is life?", "bohemian0" }, { "Mom I kill people", "bohemian1" }, { "My cat died", "bohemian2" }, { "I do not think clams stop rusting", "bohemian3" }, { "Boobs, ooh", "bohemian4" }, { "Lazy Shifutoresu Podly make le Spanish dance", "bohemian5" }, { "I have saved the life of a monster", "bohemian6" }, { "Oh Capitol principle mom django", "bohemian7" }, { "That Beelzebub Behind my bedroom ***ing", "bohemian8" }, { "It is very important to have one D", "bohemian9" } },
							new string[,]{ { "Blue Mountains, Shenanda", "countryroads0" }, { "Hour of cake, cake", "countryroads1" }, { "Road trip I am at home", "countryroads2" }, { "Where am I?", "countryroads3" }, { "Virgins of the West mount up!", "countryroads4" }, { "The lady is a miner with green juice", "countryroads5" }, { "Throw antiques and dirt in the air", "countryroads6" }, { "Cry for the third time this month", "countryroads7" }, { "Set aside time to scream", "countryroads8" }, { "Go to your room", "countryroads9" } },
							new string[,]{ { "My equality, how much love, how to put it", "despacito0" }, { "You can spend a long time in a very short time", "despacito1" }, { "I killed without problems", "despacito2" }, { "I will show you something in your ear", "despacito3" }, { "Let’s sign a labyrinth wall", "despacito4" }, { "I see your chest hair, I want to be your objective", "despacito5" }, { "Vulnerability to Secure the Danger Zone", "despacito6" }, { "I would, I wish, I want to know down the fullness of I love art", "despacito7" }, { "By steps, soft and flabby stage", "despacito8" }, { "Retro, unknown, coastline, inside, profile, lit", "despacito9" } },
							new string[,]{ { "Hawaii, he he he", "havana0"}, {"This, I'm going to hell (Oh, no)", "havana1"}, {"Yes, but my heart is in the egg", "havana2"}, {"Moths with headphones", "havana3"}, {"Au-au-au-au-au-au-au-au", "havana4"}, {"Jyephphyer", "havana5"}, {"ATLANTA WILL BE INTERESTING FOR THE STUTTGART", "havana6"}, {"Cars sound like a trap", "havana7"}, {"Satan takes me to the pasta", "havana8"}, {"Open space, closed space B", "havana9"} },
							new string[,]{ { "I am not prepared and I have no clothes", "idontcare0"}, {"Holiday does not match with confidence", "idontcare1"}, {"I want you to have a child", "idontcare2"}, {"We can control the lightning chain", "idontcare3"}, {"Taxis in hell cost 6$ per-minute", "idontcare4"}, {"We do not want to be dinner", "idontcare5"}, {"Education? It's nice for me to think about it", "idontcare6"}, {"Yes, everyone is right, yes", "idontcare7"}, {"I do not like you, you are so beautiful", "idontcare8"}, {"Aeeeeeeeeeeeeeeee", "idontcare9"} },
							new string[,]{ { "Ah fire ah", "intothe0"}, {"I can hear you but I can't hear you", "intothe1"}, {"I hope you don't care if I insult you", "intothe2"}, {"I'm afraid of horses in Finland", "intothe3"}, {"Fight with strangers", "intothe4"}, {"Oh no, the saucepan!", "intothe5"}, {"What does the kitchen want?", "intothe6"}, {"The Mine-like bombs and the salads", "intothe7"}, {"I have no idea where I want to be", "intothe8"}, {"Pineapple? (Wow)", "intothe9"} },
							new string[,]{ { "However, India (Oh!)", "look0"}, {"Honey, I'm dead and I have been all the time", "look1"}, {"Immediately I sprinkle her, and I'm doubled at that time, she!", "look2"}, {"Oh, what am I doing?", "look3"}, {"I do not like your royal buttons", "look4"}, {"You locked me and eats me (What?)", "look5"}, {"The next day, in the world, another play plays", "look6"}, {"But I do not know, I think that action is everything I think", "look7"}, {"I want to activate your star", "look8"}, {"You are now a TV", "look9"} },
							new string[,]{ { "Moon dementia?", "memory0"}, {"I'm a yippeudago", "memory1"}, {"I know he does not cheat, but he will", "memory2"}, {"The beer is back", "memory3"}, {"It seems that the fire is not strong enough", "memory4"}, {"Purple purple", "memory5"}, {"The previous day was tomorrow", "memory6"}, {"Moses smokes outside", "memory7"}, {"Dead birds; as well as the editor", "memory8"}, {"Don't let me know happiness", "memory9"} },
							new string[,]{ { "I die, and I understand that it is always coming", "mother0"}, {"Cash! I believe in animals", "mother1"}, {"Money is good", "mother2"}, {"This is a terrorist country", "mother3"}, {"Birds and thorshis, Poor Foods, Ikea", "mother4"}, {"Come on Lee booty drop dead!", "mother5"}, {"Stand still, and shut up strangers", "mother6"}, {"Violence can not be used, the trial insufficient, can not be done", "mother7"}, {"Simple, pure, straight trash", "mother8"}, {"I'm on a bus. I like it!", "mother9"},},
							new string[,]{ { "McCain has failed me", "newrules0"}, {"Frequently Asked Questions", "newrules1"}, {"Get up early in the morning in the fields of worship", "newrules2"}, {"Synchronization has left me underwater", "newrules3"}, {"Putin will continue", "newrules4"}, {"They know they are just alcohol and bamboo", "newrules5"}, {"You know that you are going to marry her in the morning", "newrules6"}, {"Look at Muslims", "newrules7"}, {"Wow shellfish, wow,", "newrules8"}, {"Do not make friends, no friends", "newrules9"} },
							new string[,]{ { "Your kidneys are not left", "notears0"}, {"So, I'll kill him, I want to fetch", "notears1"}, {"I woke up because I'm alive", "notears2"}, {"I have died, but boy, I'm happy, happy, happy", "notears3"}, {"Decide if you have a mouth", "notears4"}, {"Now, I have a suggestion Give me all the honor", "notears5"}, {"I want, I'm living, I'm up (Oh s***)", "notears6"}, {"Enchanting, those pampers are haunted", "notears7"}, {"We're here, we're weenzinsin", "notears8"}, {"Tequila", "notears9"} },
							new string[,]{ { "I'm going to dance until I don't exist", "oldtown0"}, {"I'm no longer on a hamburger ride", "oldtown1"}, {"Camelschoffel is confirmed", "oldtown2"}, {"TRICYCLE SPAREPARTS is a horse, day", "oldtown3"}, {"Now you are a door", "oldtown4"}, {"No, no day, I come to the torpedoes can refuse", "oldtown5"}, {"Satans tractors are coming", "oldtown6"}, {"My son cheated on me", "oldtown7"}, {"Children are used: they pay for weapons", "oldtown8"}, {"I don't want to move again", "oldtown9"} },
							new string[,]{ { "I want to serve the Senate", "senorita0"}, {"Oh do not touch the cat", "senorita1"}, {"Oh, need to wake up Oh, you're still asleep", "senorita2"}, {"His body fits into my ducks", "senorita3"}, {"He did not claim to be a lover of Pastirago", "senorita4"}, {"Yes, honey, that's funny! Should we talk? Tea truck", "senorita5"}, {"Dear, you killed a child", "senorita6"}, {"The real story is la la la la la la la la la la la la", "senorita7"}, {"Hi, there is something wrong with you", "senorita8"}, {"We hope this makes sense", "senorita9"} },
							new string[,]{ { "We begin to dance, and now I want to sing", "shape0"}, {"As a virgin, you and I know that I want to love you", "shape1"}, {"Do not worry about the demon", "shape2"}, {"I'm in love with your system", "shape3"}, {"Now my lines, such as spices", "shape4"}, {"Every day I find some new railway", "shape5"}, {"You and I have tightly linked the agricultural economy and all the activated meals It fills his pocket, blessing all verbs", "shape6"}, {"Taxi ride, please kiss the back seat", "shape7"}, {"Facebook knows the driver radio. I am the Confederate army.", "shape8"}, {"Gradually watermelon", "shape9"} },
							new string[,]{ { "She and the big bee-pigeon is more fragile than me", "sucker0"}, {"Burgers feel warm in December when jumping boxes", "sucker1"}, {"I will walk behind you in the dark, do not worry", "sucker2"}, {"With the black eggs you know it's clear", "sucker3"}, {"I will nurse for you", "sucker4"}, {"Blind dictionary", "sucker5"}, {"Subconscious eating everything", "sucker6"}, {"No peace and happiness", "sucker7"}, {"At the night in the kitchen there is no clothes", "sucker8"}, {"Motorcycle support", "sucker9"} },
							new string[,]{ { "Every one knows the signify unfaithfulness", "takeme0"}, {"My church provides no positivity", "takeme1"}, {"I are transmitted to heaven", "takeme2"}, {"Amine. Amine. Amine.", "takeme3"}, {"Or, those immortal dead requirements", "takeme4"}, {"I love the Holy Land is that your dog", "takeme5"}, {"Give me eternal doom", "takeme6"}, {"If I am an atheist, good time", "takeme7"}, {"Do you have a stable?", "takeme8"}, {"We have viscous purity exceeding supply sin", "takeme9"} },
							new string[,]{ { "Unfortunately, we are very wealthy", "middle0"}, {"We know that Christ died for all who have good intentions", "middle1"}, {"Hey, why are you here?", "middle2"}, {"Why can not I Phillip", "middle3"}, {"How did we have a salad? They are very aggressive", "middle4"}, {"Why are you crazy?", "middle5"}, {"I'm a little stupid", "middle6"}, {"And why should we find the body not in the middle of f?", "middle7"}, {"Go inside me", "middle8"}, {"Golf, no, no", "middle9"} },
							new string[,]{ { "With the production of wine", "soundof0"}, {"I mean, my eyes are nice", "soundof1"}, {"Thousands of millions of transformers, perhaps more", "soundof2"}, {"The man who recorded the song did not share this", "soundof3"}, {"Break the Beep", "soundof4"}, {"I would like cancer", "soundof5"}, {"But these words are like a silky whiskey", "soundof6"}, {"People worship their worship and worship", "soundof7"}, {"Merger and explosion", "soundof8"}, {"Toast", "soundof9"} },
							new string[,]{ { "State of Arizona! State of Arizona!", "thisis00"}, {"Kill people better!", "thisis01"}, {"Betty, all the cookies", "thisis02"}, {"The city of San Jose", "thisis03"}, {"Paul is a white man", "thisis04"}, {"Arizona! Arizona! Arizona! Arizona!", "thisis05"}, {"I think everyone is awesome", "thisis06"}, {"Adam was a trash bin in the corner", "thisis07"}, {"Rouge & Black, Green Smoothie", "thisis08"}, {"Do not be yourself", "thisis09"}, {"Use the opportunity and shake the cake", "thisis10"}, {"I am a prostitute and I am sorry", "thisis11"}, {"Who am I? He said: Who is he?", "thisis12"}, {"I am afraid am afraid of my hair", "thisis13"}, {"I have an umbrella", "thisis14"}, {"Fill your dreams of terrorism", "thisis15"}, {"Life is not enough", "thisis16"}, {"This is our relationship, but we are not talking", "thisis17"}, {"It is now - We don't want to do this right now", "thisis18"}, {"Get it like a rabbit. Leather store", "thisis19"} },
							new string[,]{ { "The darkness is a kitten", "thriller00"}, {"Make sure the musical instrument is even more serious than your performance", "thriller01"}, {"It ganggarita", "thriller02"}, {"Because it's fun, great joy", "thriller03"}, {"You know this erotic excitement", "thriller04"}, {"Popcorn", "thriller05"}, {"And think about a woman who is thinking", "thriller06"}, {"Superdita", "thriller07"}, {"Pine trees can not be avoided this time", "thriller08"}, {"(Widely available) of the ship", "thriller09"}, {"I will smite you", "thriller10"}, {"This is wonderful, it's funny The fact that we are dying", "thriller11"}, {"Wowie night", "thriller12"}, {"Then enter the condom's eye into the body", "thriller13"}, {"Happiness is bad", "thriller14"} },
							new string[,]{ { "I am second to God", "truth0"}, {"Please call me immediately before sending a text message", "truth1"}, {"My best friend lives on a deck chair", "truth2"}, {"Lightbulb photos with cats", "truth3"}, {"Boom burst house to bay in destruction of explosion", "truth4"}, {"Yes, I am very happy that I am coming with a cat", "truth5"}, {"You will never have a chicken biting you", "truth6"}, {"What are the benefits of this dam?", "truth7"}, {"I know less about money than debt", "truth8"}, {"We never stopped flipping like a mouse", "truth9"} },
							new string[,]{ { "It O Beach! Spirit are in", "yourewelcome0"}, {"How can you not say Never mind!", "yourewelcome1"}, {"For your mother, the sun in the sky?", "yourewelcome2"}, {"I'm not normally a German man!", "yourewelcome3"}, {"Hey, he a twig and draw the sky", "yourewelcome4"}, {"Be creative and enjoy your day", "yourewelcome5"}, {"I had blood, I blew them", "yourewelcome6"}, {"Knowing a tree is now important", "yourewelcome7"}, {"I looked at her with malignant melanoma", "yourewelcome8"}, {"Ha, ha, ha, ha, ha, ha, okay!", "yourewelcome9"} }
	};

	//Logging
	static int moduleIdCounter = 1;
	int moduleId;
	private bool moduleSolved;

	//Twitch help message
#pragma warning disable 414
	private readonly string TwitchHelpMessage = @"Submit your answer with “!{0} submit <song name>”.";
#pragma warning restore 414

	public IEnumerator ProcessTwitchCommand(string command)
	{
		command = command.ToLowerInvariant().Trim();

		if (Regex.IsMatch(command, @"^submit .+$"))
		{
			//Gets the part of the string containing the song input
			string input = command.Substring(7);

			if (input == "senorita")
			{
				input = "señorita";
			}

			//Cycle through the song list until you get the desired song.
			for (int i = 0; i < 25; i++)
			{
				if (song.text.ToLowerInvariant() != input)
				{
					yield return null;
					right.OnInteract();
					yield return new WaitForSecondsRealtime(0.1f);
				}
			}

			//However, if you cycle through all the different posibilities without the desired one appearing then send an error message 
			if (song.text.ToLowerInvariant() != input)
			{
				yield return "sendtochaterror The inputted song is not available.";
			}

			//Once you have set the display to the desired output then submit
			yield return null;
			submit.OnInteract();
		}
		else
		{
			//If the command sent isn't valid send an error
			yield return "sendtochaterror The inputted command is not valid.";
		}
	}

	void Awake()
	{
		lyric.text = "";
		song.text = "";

		//More logging stuff
		moduleId = moduleIdCounter++;

		//Sets up the methods for both buttons
		left.OnInteract += delegate () { LeftPress(); return false; };
		right.OnInteract += delegate () { RightPress(); return false; };
		submit.OnInteract += delegate () { Submit(); return false; };

		GetComponent<KMBombModule>().OnActivate += OnActivate;
	}

	//Done when the bomb starts
	void OnActivate()
	{
		//Randomises the song displayed at the bottom
		currentSong = Random.Range(0, 25);
		song.text = songNames[currentSong];

		StartCoroutine(UpdateStages());
	}

	IEnumerator UpdateStages()
	{
		//Runs for 3 stages
		for (int i = 0; i < 3; i++)
		{
			Debug.LogFormat("[Gradually Watermelon #{0}] Stage {1}:", moduleId, i + 1);

			GenerateStage();

			//Repeatedly checks if the defuser has solved the stage
			while (stage == i)
			{
				yield return null;
			}
		}

		lyric.text = "Module Solved";
		song.text = "Congrats";

		//After 3 stages the bomb solves
		GetComponent<KMBombModule>().HandlePass();
		moduleSolved = true;
	}

	void GenerateStage()
	{
		//Chooses a song and a lyric at random
		selectedSong = Random.Range(0, 25);
		selectedLyric = Random.Range(0, lyrics[selectedSong].Length / 2);

		//displays the lyric on the screen
		lyric.text = lyrics[selectedSong][selectedLyric, 0];

		Debug.LogFormat("[Gradually Watermelon #{0}] \"{1}\" is from the song \"{2}\".", moduleId, lyrics[selectedSong][selectedLyric, 0], songNames[selectedSong]);
	}

	void LeftPress()
	{
		if (moduleSolved) return;

		//Makes the bomb move when you press it
		left.AddInteractionPunch();

		//Makes a sound when you press the button
		audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);

		//Goes left 1 the list
		currentSong = (currentSong + 24) % 25;
		song.text = songNames[currentSong];
	}

	void RightPress()
	{
		if (moduleSolved) return;

		//Makes the bomb move when you press it
		left.AddInteractionPunch();

		//Makes a sound when you press the button
		audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);

		//Goes right 1 the list
		currentSong = (currentSong + 1) % 25;
		song.text = songNames[currentSong];
	}

	void Submit()
	{
		if (moduleSolved) return;

		//Makes the bomb move when you press it
		left.AddInteractionPunch();

		//Makes a sound when you press the button
		audio.PlayGameSoundAtTransform(KMSoundOverride.SoundEffect.ButtonPress, transform);

		Debug.LogFormat("[Gradually Watermelon #{0}] You submitted {1}.", moduleId, song.text);

		if (song.text == songNames[selectedSong])
		{
			audio.PlaySoundAtTransform(lyrics[selectedSong][selectedLyric, 1], transform);
			stage += 1;
			Debug.LogFormat("[Gradually Watermelon #{0}] Correct!", moduleId);
		}
		else
		{
			GetComponent<KMBombModule>().HandleStrike();
			Debug.LogFormat("[Gradually Watermelon #{0}] Incorrect! Displaying new quote.", moduleId);
			GenerateStage();
		}
	}
}