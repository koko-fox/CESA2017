using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class TFSound
{
	#region inner classes
	static class ChannelAllocator
	{
		#region inner classes
		public class Channel
		{
			public int id;
			public bool used;
			public GameObject channelObject;
			public AudioSource audioSrc;
			public Transform target;
		}
		#endregion

		#region private fields
		static bool _initialized = false;

		static GameObject _channelsParent;

		static int _capacity = 64;
		static Channel[] _channels;
		#endregion

		#region properties
		/// <summary>初期化されたか</summary>
		public static bool initialized
		{
			get { return _initialized; }
		}
		#endregion

		#region public methods
		/// <summary>初期化を行う</summary>
		public static void Initialize()
		{
			_channelsParent = new GameObject("ChannelParent@ChannelAllocator");
			GameObject.DontDestroyOnLoad(_channelsParent);

			_channels = new Channel[_capacity];
			for (int f1 = 0; f1 < _capacity; f1++)
			{
				Channel item = new Channel();
				item.channelObject = new GameObject("Channel" + f1.ToString());
				item.id = f1;
				item.used = false;
				item.audioSrc = item.channelObject.AddComponent<AudioSource>();
				item.channelObject.transform.parent = _channelsParent.transform;
				_channels[f1] = item;
			}
			_initialized = true;
		}

		/// <summary>チャンネルの割り当てを行う</summary>
		/// <param name="clip">オーディオクリップ</param>
		/// <returns>割り当てられたチャンネル</returns>
		public static Channel Allocate(AudioClip clip)
		{
			Channel unused = null;

			for(int f1=0;f1<_capacity;f1++)
			{
				if (_channels[f1].used)
					continue;

				_channels[f1].used = true;
				_channels[f1].audioSrc.clip = clip;
				unused = _channels[f1];
				break;
			}

			return unused;
		}

		/// <summary>チャンネル使用状況の更新</summary>
		public static void Checkout()
		{
			for(int f1=0;f1<_capacity;f1++)
			{
				_channels[f1].used = _channels[f1].audioSrc.isPlaying;
			}

			int count = _channels.Count(elem => elem.used);
			var usageKeys = _channels.Where(elem => elem.used);
			var keys = usageKeys.ToArray();

			TFDebug.ClearMonitor("@sound");
			TFDebug.Write("@sound", string.Format("now using {0}/{1} channels\n", count.ToString(), _capacity.ToString()));
			for (int f1 = 0; f1 < keys.Count(); f1++)
			{
				TFDebug.Write("@sound", keys[f1].audioSrc.clip.name + "\n");
			}

		}

		public static void Update()
		{
			Checkout();
			for (int f1 = 0; f1 < _capacity; f1 ++)
			{
				if (_channels[f1].target)
				{
					_channels[f1].channelObject.transform.position = _channels[f1].target.position;
				}
			}
		}

		/// <summary>チャンネルをIDから取得</summary>
		/// <param name="id">ID</param>
		/// <returns>チャンネル</returns>
		public static Channel GetChannel(int id)
		{
			return _channels[id];
		}

		/// <summary>チャンネルの配列を取得</summary>
		/// <returns>チャンネルの配列</returns>
		public static Channel[] GetChannels()
		{
			return _channels;
		}
		#endregion

	}

	class Updater:MonoBehaviour
	{
		void Update()
		{
			ChannelAllocator.Update();	
		}
	}
	#endregion

	#region private fields
	static AudioSource _audioSrc = null;
	static Dictionary<string,AudioClip> _audioDict = new Dictionary<string,AudioClip>();
	static GameObject _updater = null;
	#endregion

	#region public methods
	/// <summary>サウンドをロードする</summary>
	/// <param name="key">サウンドのキー</param>
	/// <param name="filename">ファイル名</param>
	public static void Load(string key,string filename)
	{
		_audioDict.Add(key, (AudioClip)Resources.Load(filename));
	}

	public static void Load(string key,AudioClip clip)
	{
		_audioDict.Add(key, clip);
	}

	/// <summary>サウンドを再生する</summary>
	/// <param name="key">サウンドのキー</param>
	/// <param name="loop">ループするか</param>
	/// <returns>チャンネルID</returns>
	public static int Play(string key, bool loop = false, float spatialBlend = 0f, Transform trackingTarget = null)
	{
		if (!ChannelAllocator.initialized)
		{
			ChannelAllocator.Initialize();
		}

		if(!_updater)
		{
			_updater = new GameObject("Updater@TFSound");
			GameObject.DontDestroyOnLoad(_updater);
			_updater.AddComponent<Updater>();
		}

		var channel = ChannelAllocator.Allocate(_audioDict[key]);
		if (channel != null)
		{
			channel.audioSrc.Play();
			channel.audioSrc.loop = loop;
			channel.audioSrc.spatialBlend = spatialBlend;
			channel.target = trackingTarget;
			return channel.id;
		}

		return -1;
	}

	/// <summary>指定座標でサウンドを再生する</summary>
	/// <param name="key">サウンドのキー</param>
	/// <param name="position">再生する座標</param>
	/// <param name="loop">ループするか</param>
	/// <param name="spatialBlend">3Dブレンド(0f~1f)</param>
	/// <returns>チャンネルID</returns>
	public static int PlayAtPoint(string key,Vector3 position, bool loop = false, float spatialBlend = 1f)
	{
		if (!ChannelAllocator.initialized)
		{
			ChannelAllocator.Initialize();
		}

		if (!_updater)
		{
			_updater = new GameObject("Updater@TFSound");
			GameObject.DontDestroyOnLoad(_updater);
			_updater.AddComponent<Updater>();
		}

		var channel = ChannelAllocator.Allocate(_audioDict[key]);
		if (channel != null)
		{
			channel.audioSrc.Play();
			channel.audioSrc.loop = loop;
			channel.audioSrc.spatialBlend = spatialBlend;
			channel.channelObject.transform.position = position;
			channel.target = null;
			return channel.id;
		}

		return -1;
	}

	/// <summary>サウンドの再生を停止する</summary>
	/// <param name="channelId">停止するチャンネルID</param>
	public static void Stop(int channelId)
	{
		var channel=ChannelAllocator.GetChannel(channelId);
		channel.audioSrc.Stop();
		channel.used = false;
	}

	/// <summary>全てのサウンドの再生を停止する</summary>
	public static void StopAll()
	{
		var channels=ChannelAllocator.GetChannels();
		for(int f1=0;f1<channels.Length;f1++)
		{
			channels[f1].audioSrc.Stop();
			channels[f1].used = false;
		}
	}
	#endregion
}