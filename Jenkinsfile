pipeline {
    agent any

    environment {
        DOTNET_CLI_HOME = "/tmp/DOTNET_CLI_HOME"
        SONAR_HOST_URL = 'http://sonarqube:9000'  // Adjust this to your SonarQube URL
    }

    tools {
        dotnetsdk 'dotnet-6.0'  // Make sure this matches your Jenkins tools configuration
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('SonarQube Begin') {
            steps {
                withSonarQubeEnv('Cs Coverage') {  // Configure this name in Jenkins
                    sh '''
                        dotnet tool install --global dotnet-sonarscanner
                        dotnet sonarscanner begin \
                            /k:"CSharpCodeCoverage" \
                            /d:sonar.host.url="${SONAR_HOST_URL}" \
                            /d:sonar.cs.opencover.reportsPaths="**/TestResults/Coverage/coverage.opencover.xml" \
                            /d:sonar.cs.vstest.reportsPaths="**/TestResults/*.trx"
                    '''
                }
            }
        }

        stage('Build') {
            steps {
                sh 'dotnet build --configuration Release'
            }
        }

        stage('Test with Coverage') {
            steps {
                sh '''
                    dotnet test \
                        --configuration Release \
                        --no-build \
                        --logger "trx;LogFileName=test_results.trx" \
                        /p:CollectCoverage=true \
                        /p:CoverletOutputFormat=opencover \
                        /p:CoverletOutput="./TestResults/Coverage/"
                '''
            }
        }

        stage('SonarQube End') {
            steps {
                withSonarQubeEnv('SonarQube') {
                    sh 'dotnet sonarscanner end'
                }
            }
        }
    }

    post {
        always {
            junit '**/TestResults/*.trx'
            recordCoverage(tools: [[parser: 'COBERTURA', pattern: '**/TestResults/Coverage/coverage.cobertura.xml']])
        }
    }
}
