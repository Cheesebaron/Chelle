# Chelle
A port of https://github.com/jtribe/shelly to Xamarin.Android

Chelle is a Fluent API for some common Intent use cases in Android.

## Installation
Install it from NuGet.

    PM > Install-Package Chelle
	
## Usage

### Sharing

Sharing text and url. The url is appended to the text.

```
Chelle.Share(context)
    .Text("A text with an url")
	.Url("http://ostebaronen.dk")
	.Send();
```

Sharing text and an image.

```
Chelle.Share(context)
    .Text("A text with an image")
	.Image(imageUri)
	.Send();
```

Sharing text and video.

```
Chelle.Share(context)
    .Text("A text with a video")
	.Video(videoUri)
	.Send();
```

### Sending Emails

Sending email to a single person

```
Chelle.Email(context)
    .To("some@email.com")
	.Subject("Hello from Chelle")
	.Body("Hey there, bla bla bla")
	.Send();
```

Sending email to multiple people

```
Chelle.Email(context)
    .To(new string[] {"some@email.com", "other@email.com", "another@one.com"})
	.Subject("Hello from Chelle")
	.Body("Hey there, bla bla bla")
	.Send();
```

With a Carbon Copy

```
Chelle.Email(context)
    .To(new string[] {"some@email.com", "other@email.com", "another@one.com"})
	.Cc("some@ccer.com")
	.Subject("Hello from Chelle")
	.Body("Hey there, bla bla bla")
	.Send();
```

With multiple Carbon Copies

```
Chelle.Email(context)
    .To(new string[] {"some@email.com", "other@email.com", "another@one.com"})
	.Cc(new string[] {"some@ccer.com", "other@ccer.com", "another@ccer.com"})
	.Subject("Hello from Chelle")
	.Body("Hey there, bla bla bla")
	.Send();
```

With a Blind Carbon Copy

```
Chelle.Email(context)
    .To(new string[] {"some@email.com", "other@email.com", "another@one.com"})
	.Bcc("some@bccer.com")
	.Subject("Hello from Chelle")
	.Body("Hey there, bla bla bla")
	.Send();
```

With multiple Blind Carbon Copies

```
Chelle.Email(context)
    .To(new string[] {"some@email.com", "other@email.com", "another@one.com"})
	.Bcc(new string[] {"some@bccer.com", "other@bccer.com", "another@bccer.com"})
	.Subject("Hello from Chelle")
	.Body("Hey there, bla bla bla")
	.Send();
```

### Issues and Feedback
Please use [Github Issues](https://github.com/Cheesebaron/Chelle/issues "Github Issues") for feature requests or bug reports.

### License
Licensed under the Apache License, Version 2.0 (the "License"); you may not use this file except in compliance with the License. You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the specific language governing permissions and limitations under the License.
