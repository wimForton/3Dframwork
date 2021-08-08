using System;
using System.Numerics;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GLFW;
using static GameEngine.OpenGL.GL;

namespace GameEngine
{
    class ShaderPhong
    {
        private string vertexCode;
        private string fragmentCode;
        public uint ProgramID { get; set; }

        public ShaderPhong()
        {
            vertexCode = @"#version 330 core
                            layout (location = 0) in vec3 aPosition;
                            layout(location = 1) in vec2 vertexUV; 
                            layout (location = 2) in vec3 aNormal;

                            out vec3 FragPos;
                            out vec2 UV;
                            out vec3 Normal;


                            uniform mat4 model;
                            uniform mat4 view;
                            uniform mat4 projection;

                            void main()
                            {
                                FragPos = vec3(model * vec4(aPosition, 1.0));
                                Normal = aNormal;
                                UV = vertexUV;
                                gl_Position = projection * view * vec4(aPosition, 1.0);
                                //gl_Position = projection * vec4(aPosition, 1.0);
                                //gl_Position = projection * vec4(FragPos, 1.0);

                            }";

            fragmentCode = @"#version 330 core
                            out vec4 FragColor;

                            in vec3 Normal;  
                            in vec3 FragPos;  
                            in vec2 UV;
                            uniform vec3 lightPos; 
                            uniform vec3 viewPos; 
                            uniform vec3 lightColor;
                            uniform vec3 objectColor;

                            uniform sampler2D myTextureSampler; 

                            void main()
                            {
                                //textureColor
                                vec3 texColor = texture( myTextureSampler, UV ).rgb;   

                                // ambient
                                float ambientStrength = 0.5;
                                vec3 ambient = ambientStrength * texColor;

                                // diffuse 
                                vec3 norm = normalize(Normal);
                                vec3 lightDir = normalize(lightPos - FragPos);
                                float diff = max(dot(norm, lightDir), 0.0);
                                vec3 diffuse = diff * lightColor;
    
                                // specular
                                float specularStrength = 0.8;
                                vec3 viewDir = normalize(viewPos - FragPos);
                                vec3 reflectDir = reflect(-lightDir, norm);  
                                float spec = pow(max(dot(viewDir, reflectDir), 0.0), 16);
                                vec3 specular = specularStrength * spec * lightColor;  
        
                                vec3 result = (ambient + diffuse + specular) * objectColor * texColor;
                                FragColor = vec4(result, 1.0);
                            }";
        }
        public void Load()
        {

            uint vs, fs;
            vs = glCreateShader(GL_VERTEX_SHADER);
            glShaderSource(vs, vertexCode);
            glCompileShader(vs);
            int[] status = glGetShaderiv(vs, GL_COMPILE_STATUS, 1);
            if(status[0] == 0)
            {
                //failed to compile
                string error = glGetShaderInfoLog(vs);
                Debug.WriteLine("error compiling vertex shader" + error);
            }

            fs = glCreateShader(GL_FRAGMENT_SHADER);
            glShaderSource(fs, fragmentCode);
            glCompileShader(fs);
            status = glGetShaderiv(vs, GL_COMPILE_STATUS, 1);
            if (status[0] == 0)
            {
                //failed to compile
                string error = glGetShaderInfoLog(vs);
                Debug.WriteLine("error compiling vertex shader" + error);
            }
            //use our shader in the program
            ProgramID = glCreateProgram();
            glAttachShader(ProgramID, vs);
            glAttachShader(ProgramID, fs);

            glLinkProgram(ProgramID);

            //delete shaders (we don't need them anymore)
            glDetachShader(ProgramID, vs);
            glDetachShader(ProgramID, fs);
            glDeleteShader(vs);
            glDeleteShader(fs);
        }
        public void Use()
        {
            glUseProgram(ProgramID);
        }
        public void SetMatrix4x4(string uniformName, Matrix4x4 mat)
        {
            int location = glGetUniformLocation(ProgramID, uniformName);
            glUniformMatrix4fv(location, 1, false, GetMatrix4x4Values(mat));
        }
        public void SetVec3(string uniformName, Vector3 vec)
        {
            int location = glGetUniformLocation(ProgramID, uniformName);
            glUniform3f(location, vec.X, vec.Y, vec.Z);
        }
        private float[] GetMatrix4x4Values(Matrix4x4 m)
        {
            return new float[]
            {
        m.M11, m.M12, m.M13, m.M14,
        m.M21, m.M22, m.M23, m.M24,
        m.M31, m.M32, m.M33, m.M34,
        m.M41, m.M42, m.M43, m.M44
            };
        }
    }
}
